﻿using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.WriteModel.Domain.Activities.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Activities
{
    internal sealed class ActivityEventHandler : INotificationHandler<PercentageActivityCreatedEvent>, INotificationHandler<ActivityPreviewImageAssigned>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public ActivityEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<ActivityPreviewImageAssigned> Members

        public async Task Handle(ActivityPreviewImageAssigned notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var activityRepository = new ActivityRepository();
                await activityRepository.AssignPreviewImageAsync(connection, notification.AggregateId, notification.PreviewImageId);
            }
        }

        #endregion

        #region INotificationHandler<PercentageActivityCreatedEvent> Members

        public async Task Handle(PercentageActivityCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var activityRepository = new ActivityRepository();
                await activityRepository.CreateNewAsync(connection, notification.AggregateId, notification.Name, ActivityType.Percentage);
            }
        }

        #endregion
    }
}