using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Lanes.Repository;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Lanes
{
    internal sealed class LaneEventHandler : INotificationHandler<LaneCreatedEvent>,
        INotificationHandler<TaskAddedEvent>,
        INotificationHandler<TaskRemovedEvent>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public LaneEventHandler(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        #region INotificationHandler<LaneCreatedEvent> Members

        public async Task Handle(LaneCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var laneRepository = new LaneRepository();
                await laneRepository.InsertAsync(connection, new LaneEntity
                {
                    LaneId = notification.AggregateId,
                    Name = notification.Name,
                    IsCompletedLane = notification.IsCompletedLane,
                    IsRejectedLane = notification.IsRejectedLane,
                    ColorCode = notification.ColorCode,
                    RunningNumberId = notification.RunningNumberId
                });
            }
        }

        #endregion

        #region INotificationHandler<TaskAddedEvent> Members

        public async Task Handle(TaskAddedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var laneRepository = new LaneTaskRepository();
                await laneRepository.AddTaskToLaneAsync(connection, notification.TaskId, notification.AggregateId,
                    notification.OrderNumber);
            }
        }

        #endregion

        #region INotificationHandler<TaskRemovedEvent> Members

        public async Task Handle(TaskRemovedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var laneRepository = new LaneTaskRepository();
                await laneRepository.RemoveTaskFromLaneAsync(connection, notification.TaskId, notification.AggregateId);
            }
        }

        #endregion
    }
}