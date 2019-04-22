using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Lanes
{
    internal sealed class LaneEventHandler : IEventHandler<LaneCreatedEvent>
    {
        #region IEventHandler<LaneCreatedEvent> Members

        public void Handle(LaneCreatedEvent @event)
        {
        }

        #endregion
    }
}