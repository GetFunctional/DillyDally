using System;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryCommandHandler : CommandHandlerBase, ICommandHandler<CreateCategoryCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public CategoryCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateCategoryCommand> Members

        public IAggregateRoot Handle(CreateCategoryCommand command)
        {
            var categoryId = this.GuidGenerator.GenerateGuid();
            var runningNumberId = this.CreateNewRunningNumberForCategory();

            var aggregate =  CategoryAggregateRoot.Create(categoryId, runningNumberId, command.Name, command.ColorCode);
            this._aggregateRepository.Save(aggregate);
            return aggregate;
        }

        #endregion

        private Guid CreateNewRunningNumberForCategory()
        {
            var runningNumberIdForTasks =
                RunningNumberCounterCommandHandler.AreaToIdentityMapping[RunningNumberCounterArea.Category];
            var runningNumbers =
                this._aggregateRepository.GetById<RunningNumberCounterAggregateRoot>(runningNumberIdForTasks);
            var newRunningNumberId = this.GuidGenerator.GenerateGuid();
            runningNumbers.AddNextNumber(newRunningNumberId);
            this._aggregateRepository.Save(runningNumbers);

            return newRunningNumberId;
        }
    }
}