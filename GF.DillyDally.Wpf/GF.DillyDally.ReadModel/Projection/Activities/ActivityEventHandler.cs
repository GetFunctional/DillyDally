﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Activities.Repository;
using GF.DillyDally.ReadModel.Projection.Files.Repository;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.Shared.Images;
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
                var imageRepository = new ImageRepository();
                var fileRepository = new FileRepository();

                var file = await fileRepository.GetByIdAsync(connection, notification.FileId);
                var activityId = notification.AggregateId;

                var storedImages = await imageRepository.StoreImagesAsync(connection, file);
                var previewImage = storedImages.Single(x => x.SizeType == ImageSizeType.PreviewSize);
                await activityRepository.AssignPreviewImageAsync(connection, activityId, previewImage.ImageId);
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