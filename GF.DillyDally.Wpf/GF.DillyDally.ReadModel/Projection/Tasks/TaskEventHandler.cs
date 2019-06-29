using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Files.Repository;
using GF.DillyDally.ReadModel.Projection.Images.Repository;
using GF.DillyDally.ReadModel.Projection.Tasks.Repository;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Tasks
{
    internal sealed class TaskEventHandler : INotificationHandler<TaskCreatedEvent>,
        INotificationHandler<AttachedFileToTaskEvent>, INotificationHandler<PreviewImageAssignedEvent>, INotificationHandler<TaskLinkCreatedEvent>,
        INotificationHandler<DefinitionOfDoneChangedEvent>, INotificationHandler<TaskLinkedToActivitiesEvent>
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
                    var imageRepository = new ImageRepository();
                    var taskImagesRepository = new TaskImageRepository();

                    await imageRepository.StoreImagesAsync(connection, file);
                    await taskImagesRepository.CreateTaskImageLinks(connection, taskId, file.FileId);
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

        #region INotificationHandler<DefinitionOfDoneChangedEvent> Members

        public async Task Handle(DefinitionOfDoneChangedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var repository = new TaskRepository();
                await repository.UpdateDefinitionOfDoneAsync(connection, notification.AggregateId, notification.DefinitionOfDone);
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
                var PreviewImageFileIdForFile =
                    await imageRepository.GetPreviewImageFileIdForFileAsync(connection, notification.FileId);
                var taskId = notification.AggregateId;

                await taskRepository.ChangePreviewImageAsync(connection, taskId, PreviewImageFileIdForFile);
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
                                                                 PreviewImageFileId = notification.PreviewImageFileId
                                                             });
            }
        }

        #endregion

        #region INotificationHandler<TaskLinkCreatedEvent> Members

        public async Task Handle(TaskLinkCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var repository = new TaskLinksRepository();
                await repository.CreateNewLinkbetweenTasksAsync(connection, notification.AggregateId, notification.LinkToTaskId);
            }
        }

        #endregion

        #region INotificationHandler<TaskLinkedToActivitiesEvent> Members

        public async Task Handle(TaskLinkedToActivitiesEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var repository = new TaskActivityRepository();
                await repository.LinkTaskToActivitiesAsync(connection, notification.AggregateId, notification.ActivityIds);
            }
        }

        #endregion
    }
}