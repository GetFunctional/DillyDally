using GF.DillyDally.WriteModel.Domain.Rewards.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Rewards
{
    internal sealed class RewardEventHandler : IEventHandler<RewardCreatedEvent>
    {
        #region IEventHandler<RunningNumberCounterCreatedEvent> Members

        public void Handle(RewardCreatedEvent @event)
        {
        }

        #endregion
    }
}