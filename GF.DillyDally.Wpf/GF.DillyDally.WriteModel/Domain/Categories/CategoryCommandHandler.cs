using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Categories.Commands;
using GF.DillyDally.WriteModel.Domain.RunningNumbers;
using GF.DillyDally.WriteModel.Domain.RunningNumbers.Events;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Categories
{
    internal sealed class CategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        private readonly RunningNumberFactory _runningNumberFactory;

        public CategoryCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
            this._runningNumberFactory = new RunningNumberFactory(this.AggregateRepository, this.GuidGenerator);
        }

        #region IRequestHandler<CreateCategoryCommand,CreateCategoryResponse> Members

        public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var categoryId = this.GuidGenerator.GenerateGuid();
            var runningNumberId = await
                this._runningNumberFactory.CreateNewRunningNumberForAsync(RunningNumberCounterArea.Category);

            var aggregate =
                CategoryAggregateRoot.Create(categoryId, runningNumberId, request.Name, request.ColorCode);
            await this.AggregateRepository.SaveAsync(aggregate);
            return new CreateCategoryResponse(categoryId);
        }

        #endregion
    }
}