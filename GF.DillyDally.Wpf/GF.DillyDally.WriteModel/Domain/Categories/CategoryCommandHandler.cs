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
            return await Task.Run(() =>
            {
                var categoryId = this.GuidGenerator.GenerateGuid();
                var runningNumberId =
                    this._runningNumberFactory.CreateNewRunningNumberFor(RunningNumberCounterArea.Category);

                var aggregate =
                    CategoryAggregateRoot.Create(categoryId, runningNumberId, request.Name, request.ColorCode);
                this.AggregateRepository.Save(aggregate);
                return new CreateCategoryResponse(categoryId);
            }, cancellationToken);
        }

        #endregion
    }
}