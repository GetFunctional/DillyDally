using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    internal sealed class RewardCommandHandler : CommandHandlerBase, ICommandHandler<CreateRewardCommand>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public RewardCommandHandler(IAggregateRepository aggregateRepository)
        {
            this._aggregateRepository = aggregateRepository;
        }

        #region ICommandHandler<CreateRewardCommand> Members

        public IAggregateRoot Handle(CreateRewardCommand command)
        {
            var rewardId = this.GuidGenerator.GenerateGuid();

            var aggregate = RewardAggregateRoot.Create(rewardId, command.Name, command.CurrencyCode);
            this._aggregateRepository.Save(aggregate);

            return aggregate;
        }

        #endregion
    }
}