using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Files;
using GF.DillyDally.WriteModel.Domain.Files.Commands;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal sealed class TaskCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateTaskCommand, CreateTaskResponse>,
        IRequestHandler<AttachFileToTaskCommand, AttachFileToTaskResponse>,
        IRequestHandler<AssignPreviewImageCommand, AssignPreviewImageResponse>, IRequestHandler<LinkTaskCommand, LinkTaskResponse>,
        IRequestHandler<AssignDefinitionOfDoneCommand, AssignDefinitionOfDoneResponse>, IRequestHandler<ChangeTaskLaneCommand, ChangeTaskLaneResponse>, IRequestHandler<LinkTaskToActivitiesCommand, LinkTaskToActivitiesResponse>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly RunningNumberFactory _runningNumberFactory;

        public TaskCommandHandler(IAggregateRepository aggregateRepository, DatabaseFileHandler databaseFileHandler) :
            base(aggregateRepository)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._runningNumberFactory = new RunningNumberFactory(aggregateRepository, new GuidGenerator());
        }

        #region IRequestHandler<AssignDefinitionOfDoneCommand,AssignDefinitionOfDoneResponse> Members

        public async Task<AssignDefinitionOfDoneResponse> Handle(AssignDefinitionOfDoneCommand request, CancellationToken cancellationToken)
        {
            var task = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);

            task.AssignDefinitionOfDone(request.DefinitionOfDone);
            await this.AggregateRepository.SaveAsync(task);

            return new AssignDefinitionOfDoneResponse();
        }

        #endregion

        #region IRequestHandler<AssignPreviewImageCommand,AssignPreviewImageResponse> Members

        public async Task<AssignPreviewImageResponse> Handle(AssignPreviewImageCommand request,
            CancellationToken cancellationToken)
        {
            var aggregate = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
            aggregate.AssignPreviewImage(request.FileId);
            await this.AggregateRepository.SaveAsync(aggregate);

            return new AssignPreviewImageResponse();
        }

        #endregion

        #region IRequestHandler<AttachFileToTaskCommand,AttachFileToTaskResponse> Members

        public async Task<AttachFileToTaskResponse> Handle(AttachFileToTaskCommand request,
            CancellationToken cancellationToken)
        {
            using (var connection = this._databaseFileHandler.OpenConnection())
            {
                var fileCreateCommand = new StoreFileCommand(request.FilePath);
                var fileInStore = await FileCommandHandler.GetOrCreateFileAsync(fileCreateCommand,
                    this.AggregateRepository, connection, this.GuidGenerator);

                var task = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
                task.AttachFile(fileInStore.FileId);
                await this.AggregateRepository.SaveAsync(task);
                return new AttachFileToTaskResponse(fileInStore.FileId);
            }
        }

        #endregion

        #region IRequestHandler<CreateTaskCommand,CreateTaskResponse> Members

        public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var category = this.AggregateRepository.GetById<CategoryAggregateRoot>(request.CategoryId);
            var laneList = this.AggregateRepository.GetById<LaneListAggregateRoot>(LaneListAggregateRoot.LaneListAggregateId);
            var laneId = request.LaneId != null ? laneList.GetLane(request.LaneId.Value) : laneList.GetFirstLane();
            var laneAggregate = this.AggregateRepository.GetById<LaneAggregateRoot>(laneId);
            var newRunningNumberId = await
                this._runningNumberFactory.CreateNewRunningNumberForAsync(RunningNumberCounterArea.Task);
            var taskId = this.GuidGenerator.GenerateGuid();

            // Create Task
            var aggregate = TaskAggregateRoot.CreateTask(taskId, request.Name, newRunningNumberId,
                category.AggregateId, request.PreviewImageFileId, request.StoryPoints);

            // Add Task to lane
            laneAggregate.AddTask(taskId);

            await this.AggregateRepository.SaveAsync(aggregate);
            await this.AggregateRepository.SaveAsync(laneAggregate);
            return new CreateTaskResponse(taskId);
        }

        #endregion

        #region IRequestHandler<LinkTaskCommand,LinkTaskResponse> Members

        public async Task<LinkTaskResponse> Handle(LinkTaskCommand request, CancellationToken cancellationToken)
        {
            var sourceTask = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
            var taskToLink = this.AggregateRepository.GetById<TaskAggregateRoot>(request.LinkToTaskId);

            sourceTask.LinkToTask(taskToLink.AggregateId);
            taskToLink.LinkToTask(sourceTask.AggregateId);

            await this.AggregateRepository.SaveAsync(sourceTask);
            await this.AggregateRepository.SaveAsync(taskToLink);

            return new LinkTaskResponse();
        }

        #endregion

        public async Task<ChangeTaskLaneResponse> Handle(ChangeTaskLaneCommand request, CancellationToken cancellationToken)
        {
            var taskId = request.TaskId;
            var sourceLane = this.AggregateRepository.GetById<LaneAggregateRoot>(request.SourceLaneId);
            var targetLane = this.AggregateRepository.GetById<LaneAggregateRoot>(request.DestinationLaneId);

            sourceLane.RemoveTask(taskId);
            targetLane.AddTask(taskId);

            await this.AggregateRepository.SaveAsync(sourceLane);
            await this.AggregateRepository.SaveAsync(targetLane);
            return new ChangeTaskLaneResponse();
        }

        public async Task<LinkTaskToActivitiesResponse> Handle(LinkTaskToActivitiesCommand request, CancellationToken cancellationToken)
        {
            var sourceTask = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);

            sourceTask.LinkToActivities(request.ActivityIds);

            await this.AggregateRepository.SaveAsync(sourceTask);
            return new LinkTaskToActivitiesResponse();
        }
    }
}