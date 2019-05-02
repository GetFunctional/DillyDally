using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Tasks
{
    internal sealed class TaskEventHandler : INotificationHandler<TaskCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public TaskEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

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
    }
}