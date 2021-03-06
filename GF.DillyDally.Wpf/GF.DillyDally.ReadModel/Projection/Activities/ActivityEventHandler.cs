﻿using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.ReadModel.Projection.Files.Repository;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.WriteModel.Domain.Activities.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Activities
{
    internal sealed class ActivityEventHandler : INotificationHandler<PercentageActivityCreatedEvent>,
        INotificationHandler<ActivityPreviewImageAssigned>, INotificationHandler<ActivityFieldAttached>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ActivityEventHandler(IDbConnectionFactory dbConnectionFactory)
        {
            this._dbConnectionFactory = dbConnectionFactory;
        }

        #region INotificationHandler<ActivityFieldAttached> Members

        public async Task Handle(ActivityFieldAttached notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var activityRepository = new ActivityFieldRepository();
                await activityRepository.AttachFieldToActivityAsync(connection, notification.AggregateId, notification.ActivityFieldType,
                    notification.FieldName, notification.UnitOfMeasure);
            }
        }

        #endregion

        #region INotificationHandler<ActivityPreviewImageAssigned> Members

        public async Task Handle(ActivityPreviewImageAssigned notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var activityRepository = new ActivityRepository();
                var imageRepository = new ImageRepository();
                var fileRepository = new FileRepository();

                var file = await fileRepository.GetByIdAsync(connection, notification.FileId);
                var activityId = notification.AggregateId;

                await imageRepository.StoreImagesAsync(connection, file);
                await activityRepository.AssignPreviewImageAsync(connection, activityId, notification.FileId);
            }
        }

        #endregion

        #region INotificationHandler<PercentageActivityCreatedEvent> Members

        public async Task Handle(PercentageActivityCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._dbConnectionFactory.OpenConnection())
            {
                var activityRepository = new ActivityRepository();
                await activityRepository.CreateNewAsync(connection, notification.AggregateId, notification.Name,
                    ActivityType.Percentage);
            }
        }

        #endregion
    }
}