using System;
using GF.DillyDally.WriteModel.Domain.Categories;
using GF.DillyDally.WriteModel.Domain.Lanes;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Domain.Tasks.Commands;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks
{
    internal sealed class TaskCommandHandler : CommandHandlerBase,
        ICommandHandler<CreateTaskCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public TaskCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateTaskCommand> Members

        public IAggregateRoot Handle(CreateTaskCommand command)
        {
            var category = this._aggregateRepository.GetById<CategoryAggregateRoot>(command.CategoryId);
            var lane = this._aggregateRepository.GetById<LaneAggregateRoot>(command.LaneId);
            var newRunningNumberId = this.CreateNewRunningNumberForTask();
            var taskId = this.GuidGenerator.GenerateGuid();

            var aggregate = TaskAggregateRoot.CreateTask(taskId, command.Name, newRunningNumberId,
                category.AggregateId, lane.AggregateId, command.Storypoints, command.PreviewImageId);
            this._aggregateRepository.Save(aggregate);
            return aggregate;
        }

        #endregion

        private Guid CreateNewRunningNumberForTask()
        {
            var runningNumberIdForTasks =
                RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Task];
            var runningNumbers =
                this._aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberIdForTasks);
            var newRunningNumberId = this.GuidGenerator.GenerateGuid();
            runningNumbers.AddNextNumber(newRunningNumberId);
            this._aggregateRepository.Save(runningNumbers);

            return newRunningNumberId;
        }
    }
}