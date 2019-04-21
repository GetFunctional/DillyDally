using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Rewards
{
    internal sealed class RewardEventHandler : IEventHandler<RewardCreatedEvent>
    {
        #region IEventHandler<RewardCreatedEvent> Members

        public void Handle(RewardCreatedEvent @event)
        {
        }

        #endregion
    }
}