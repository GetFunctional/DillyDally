using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.Shared.Images;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Tasks
{
    internal sealed class TaskEventHandler : INotificationHandler<TaskCreatedEvent>, INotificationHandler<AttachedFileToTaskEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public TaskEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<AttachedFileToTaskEvent> Members

        public async Task Handle(AttachedFileToTaskEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var fileRepository = new FileRepository();
                var file = await fileRepository.GetByIdAsync(connection, notification.FileId);

                if (file.IsImage)
                {
                    var imageRepository = new ImageRepository();
                    var taskImageRepository = new TaskImageRepository();
                    var guidGenerator = this._fileHandler.GuidGenerator;

                    // Create Image Previews for all sizes
                    var previewImage = CreateImageEntity(file, guidGenerator, ImageSizeType.PreviewSize);
                    var smallImage = CreateImageEntity(file, guidGenerator, ImageSizeType.Small);
                    var fullImage = CreateImageEntity(file, guidGenerator, ImageSizeType.Full);
                    var taskImageLinks = CreateTaskImageLinks(notification, guidGenerator, previewImage, smallImage, fullImage);

                    var images = new List<ImageEntity> {previewImage, smallImage, fullImage};

                    await imageRepository.InsertMultipleAsync(connection, images);
                    await taskImageRepository.InsertMultipleAsync(connection, taskImageLinks);
                }
                else
                {
                    var taskFileRepository = new TaskFileRepository();
                    await taskFileRepository.InsertAsync(connection, new TaskFileEntity
                                                                     {
                                                                         TaskFileId = this._fileHandler.GuidGenerator.GenerateGuid(),
                                                                         TaskId = notification.AggregateId,
                                                                         FileId = notification.FileId
                                                                     });
                }
            }
        }

        #endregion

        #region INotificationHandler<TaskCreatedEvent> Members

        public async Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var taskRepository = new TaskRepository();
                await taskRepository.InsertAsync(connection, new TaskEntity
                                                             {
                                                                 TaskId = notification.AggregateId,
                                                                 Name = notification.Name,
                                                                 CategoryId = notification.CategoryId,
                                                                 RunningNumberId = notification.RunningNumberId,
                                                                 CreatedOn = notification.CreatedOn,
                                                                 LaneId = notification.LaneId,
                                                                 PreviewImageId = notification.PreviewImageId
                                                             });
            }
        }

        #endregion

        private static List<TaskImageEntity> CreateTaskImageLinks(AttachedFileToTaskEvent notification, IGuidGenerator guidGenerator, ImageEntity previewImage,
            ImageEntity smallImage, ImageEntity fullImage)
        {
            return new List<TaskImageEntity>
                   {
                       CreateTaskImageEntity(notification.AggregateId, previewImage, guidGenerator),
                       CreateTaskImageEntity(notification.AggregateId, smallImage, guidGenerator),
                       CreateTaskImageEntity(notification.AggregateId, fullImage, guidGenerator)
                   };
        }

        private static TaskImageEntity CreateTaskImageEntity(Guid taskId, ImageEntity imageEntity, IGuidGenerator guidGenerator)
        {
            return new TaskImageEntity
                   {
                       TaskId = taskId,
                       ImageId = imageEntity.ImageId,
                       TaskImageId = guidGenerator.GenerateGuid()
                   };
        }

        private static ImageEntity CreateImageEntity(FileEntity file, IGuidGenerator guidGenerator, ImageSizeType imageSizeType)
        {
            return new ImageEntity
                   {
                       Binary = ImageResizer.CreateImagePreview(file.Binary, imageSizeType),
                       ImageId = guidGenerator.GenerateGuid(),
                       OriginalFileId = file.FileId,
                       SizeType = imageSizeType
                   };
        }
    }
}