using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal sealed class TaskCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateTaskCommand, CreateTaskResponse>
    {
        private readonly RunningNumberFactory _runningNumberFactory;

        public TaskCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
            this._runningNumberFactory = new RunningNumberFactory(aggregateRepository, new GuidGenerator());
        }

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
    }
}