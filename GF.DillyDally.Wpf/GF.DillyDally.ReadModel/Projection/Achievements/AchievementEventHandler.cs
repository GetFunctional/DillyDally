using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : IEventHandler<RegularAchievementCreatedEvent>
    {
        #region IEventHandler<RegularAchievementCreatedEvent> Members

        public void Handle(RegularAchievementCreatedEvent @event)
        {
        }

        #endregion
    }
}