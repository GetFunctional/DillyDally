using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : IEventHandler<AchievementCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;
        private readonly IAchievementRepository _repository;

        public AchievementEventHandler(DatabaseFileHandler fileHandler, IAchievementRepository repository)
        {
            this._fileHandler = fileHandler;
            this._repository = repository;
        }

        #region IEventHandler<AchievementCreatedEvent> Members

        public void Handle(AchievementCreatedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                this._repository.InsertAsync(connection, new Achievement
                {
                    AchievementId = @event.AggregateId,
                    Name = @event.Name,
                    CounterIncrease = @event.CounterIncrease,
                    StoryPoints = @event.Storypoints,
                    RunningNumberId = @event.RunningNumberId
                });
            }
        }

        #endregion
    }
}