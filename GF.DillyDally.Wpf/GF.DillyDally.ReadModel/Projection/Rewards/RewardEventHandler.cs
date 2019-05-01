using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Domain.Rewards.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Rewards
{
    internal sealed class RewardEventHandler : INotificationHandler<RewardCreatedEvent>
    {
        #region INotificationHandler<RewardCreatedEvent> Members

        public async Task Handle(RewardCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        #endregion
    }
}