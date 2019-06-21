using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Infrastructure;
using MediatR;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    internal sealed class RewardCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateRewardCommand, CreateRewardResponse>
    {
        public RewardCommandHandler(IAggregateRepository aggregateRepository) : base(aggregateRepository)
        {
        }

        #region IRequestHandler<CreateRewardCommand,CreateRewardResponse> Members

        public async Task<CreateRewardResponse> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
        {
            var rewardId = this.GuidGenerator.GenerateGuid();

            var aggregate = RewardAggregateRoot.Create(rewardId, request.Name, request.CurrencyCode);
            await this.AggregateRepository.SaveAsync(aggregate);

            return new CreateRewardResponse(rewardId);
        }

        #endregion
    }
}