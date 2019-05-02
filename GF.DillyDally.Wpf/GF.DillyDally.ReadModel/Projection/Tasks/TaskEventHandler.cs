using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    internal sealed class TaskEventHandler : INotificationHandler<TaskCreatedEvent>,
        INotificationHandler<AttachedFileToTaskEvent>, INotificationHandler<PreviewImageAssignedEvent>
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
                var taskId = notification.AggregateId;

                if (file.IsImage)
                {
                    await this.StoreAndLinkImageAsync(taskId, connection, file);
                }
                else
                {
                    var taskFileRepository = new TaskFileRepository();
                    await taskFileRepository.InsertAsync(connection, new TaskFileEntity
                    {
                        TaskFileId = this._fileHandler.GuidGenerator.GenerateGuid(),
                        TaskId = taskId,
                        FileId = notification.FileId
                    });
                }
            }
        }

        #endregion

        #region INotificationHandler<PreviewImageAssignedEvent> Members

        public async Task Handle(PreviewImageAssignedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var taskRepository = new TaskRepository();
                var imageRepository = new ImageRepository();
                var previewImageIdForFile =
                    await imageRepository.GetPreviewImageIdForFileAsync(connection, notification.FileId);
                var taskId = notification.AggregateId;

                await taskRepository.ChangePreviewImageAsync(connection, taskId, previewImageIdForFile);
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

        private async Task StoreAndLinkImageAsync(Guid taskId, IDbConnection connection, FileEntity file)
        {
            var imageRepository = new ImageRepository();
            var taskImageRepository = new TaskImageRepository();
            var guidGenerator = this._fileHandler.GuidGenerator;

            var imagesForFile = await imageRepository.GetByOriginalFileIdAsync(connection, file.FileId);

            // Create image previews for all sizes if necessary
            var previewImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.PreviewSize) ??
                               CreateImageEntity(file, guidGenerator, ImageSizeType.PreviewSize);
            var smallImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.Small) ??
                             CreateImageEntity(file, guidGenerator, ImageSizeType.Small);
            var fullImage = imagesForFile.FirstOrDefault(x => x.SizeType == ImageSizeType.Full) ??
                            CreateImageEntity(file, guidGenerator, ImageSizeType.Full);
            var taskImageLinks = CreateTaskImageLinks(taskId, guidGenerator, previewImage, smallImage, fullImage);

            var images = new List<ImageEntity> {previewImage, smallImage, fullImage};

            if (!imagesForFile.Any())
            {
                await imageRepository.InsertMultipleAsync(connection, images);
            }

            await taskImageRepository.InsertMultipleAsync(connection, taskImageLinks);
        }

        private static List<TaskImageEntity> CreateTaskImageLinks(Guid taskId, IGuidGenerator guidGenerator,
            ImageEntity previewImage,
            ImageEntity smallImage, ImageEntity fullImage)
        {
            return new List<TaskImageEntity>
            {
                CreateTaskImageEntity(taskId, previewImage, guidGenerator),
                CreateTaskImageEntity(taskId, smallImage, guidGenerator),
                CreateTaskImageEntity(taskId, fullImage, guidGenerator)
            };
        }

        private static TaskImageEntity CreateTaskImageEntity(Guid taskId, ImageEntity imageEntity,
            IGuidGenerator guidGenerator)
        {
            return new TaskImageEntity
            {
                TaskId = taskId,
                ImageId = imageEntity.ImageId,
                TaskImageId = guidGenerator.GenerateGuid()
            };
        }

        private static ImageEntity CreateImageEntity(FileEntity file, IGuidGenerator guidGenerator,
            ImageSizeType imageSizeType)
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