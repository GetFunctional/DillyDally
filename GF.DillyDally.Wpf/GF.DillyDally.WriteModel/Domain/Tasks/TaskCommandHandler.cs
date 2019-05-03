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
        IRequestHandler<AssignPreviewImageCommand, AssignPreviewImageResponse>, IRequestHandler<LinkTaskCommand, LinkTaskResponse>
    {
        private readonly DatabaseFileHandler _databaseFileHandler;
        private readonly RunningNumberFactory _runningNumberFactory;

        public TaskCommandHandler(IAggregateRepository aggregateRepository, DatabaseFileHandler databaseFileHandler) :
            base(aggregateRepository)
        {
            this._databaseFileHandler = databaseFileHandler;
            this._runningNumberFactory = new RunningNumberFactory(aggregateRepository, new GuidGenerator());
        }

        #region IRequestHandler<AssignPreviewImageCommand,AssignPreviewImageResponse> Members

        public async Task<AssignPreviewImageResponse> Handle(AssignPreviewImageCommand request,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var aggregate = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
                aggregate.AssignPreviewImage(request.FileId);
                this.AggregateRepository.Save(aggregate);
                return new AssignPreviewImageResponse();
            }, cancellationToken);
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
                    this.AggregateRepository, connection, this.GuidGenerator,
                    new FileRepository());

                var task = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
                task.AttachFile(fileInStore.FileId);
                this.AggregateRepository.Save(task);
                return new AttachFileToTaskResponse(fileInStore.FileId);
            }
        }

        #endregion

        #region IRequestHandler<CreateTaskCommand,CreateTaskResponse> Members

        public async Task<CreateTaskResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var category = this.AggregateRepository.GetById<CategoryAggregateRoot>(request.CategoryId);
                var lane = this.AggregateRepository.GetById<LaneAggregateRoot>(request.LaneId);
                var newRunningNumberId =
                    this._runningNumberFactory.CreateNewRunningNumberFor(RunningNumberCounterArea.Task);
                var taskId = this.GuidGenerator.GenerateGuid();

                var aggregate = TaskAggregateRoot.CreateTask(taskId, request.Name, newRunningNumberId,
                    category.AggregateId, lane.AggregateId, request.PreviewImageId);
                this.AggregateRepository.Save(aggregate);

                return new CreateTaskResponse(taskId);
            }, cancellationToken);
        }

        #endregion

        #region IRequestHandler<LinkTaskCommand,LinkTaskResponse> Members

        public async Task<LinkTaskResponse> Handle(LinkTaskCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var sourceTask = this.AggregateRepository.GetById<TaskAggregateRoot>(request.TaskId);
                var taskToLink = this.AggregateRepository.GetById<TaskAggregateRoot>(request.LinkToTaskId);

                sourceTask.LinkTo(taskToLink.AggregateId);
                taskToLink.LinkTo(sourceTask.AggregateId);

                this.AggregateRepository.Save(sourceTask);
                this.AggregateRepository.Save(taskToLink);

                return new LinkTaskResponse();
            }, cancellationToken);
        }

        #endregion
    }
}