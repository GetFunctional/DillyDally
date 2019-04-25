using GF.DillyDally.WriteModel.Domain.Rewards.Commands;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    internal sealed class RewardCommandHandler : CommandHandlerBase, ICommandHandler<CreateRewardCommand>
    {
        #region ICommandHandler<CreateRewardCommand> Members

        public IAggregateRoot Handle(CreateRewardCommand command)
        {
            var rewardId = this.GuidGenerator.GenerateGuid();

            var aggregate = RewardAggregateRoot.Create(rewardId, command.Name, command.CurrencyCode);
            return aggregate;
        }

        #endregion
    }
}