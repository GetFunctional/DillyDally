using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Tasks.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Tasks
{
    internal sealed class TaskEventHandler : IEventHandler<TaskCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;
        private readonly ITaskRepository _taskRepository;

        public TaskEventHandler(DatabaseFileHandler fileHandler, ITaskRepository taskRepository)
        {
            this._fileHandler = fileHandler;
            this._taskRepository = taskRepository;
        }

        #region IEventHandler<TaskCreatedEvent> Members

        public void Handle(TaskCreatedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                this._taskRepository.InsertAsync(connection, new Task
                                                             {
                                                                 TaskId = @event.AggregateId,
                                                                 Name = @event.Name,
                                                                 CategoryId = @event.CategoryId,
                                                                 RunningNumberId = @event.RunningNumberId,
                                                                 CreatedOn = @event.CreatedOn,
                                                                 LaneId = @event.LaneId,
                                                                 PreviewImageId = @event.PreviewImageId
                                                             });
            }
        }

        #endregion
    }
}