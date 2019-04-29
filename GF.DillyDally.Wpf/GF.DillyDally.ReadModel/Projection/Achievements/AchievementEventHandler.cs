using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : IEventHandler<AchievementCreatedEvent>,
        IEventHandler<AchievementCompletedEvent>
    {
        private readonly IAchievementCompletionRepository _achievementCompletionRepository;
        private readonly DatabaseFileHandler _fileHandler;
        private readonly IAchievementRepository _repository;

        public AchievementEventHandler(DatabaseFileHandler fileHandler, IAchievementRepository repository,
            IAchievementCompletionRepository achievementCompletionRepository)
        {
            this._fileHandler = fileHandler;
            this._repository = repository;
            this._achievementCompletionRepository = achievementCompletionRepository;
        }

        #region IEventHandler<AchievementCompletedEvent> Members

        public void Handle(AchievementCompletedEvent @event)
        {
            var guidGenerator = this._fileHandler.GuidGenerator;

            using (var connection = this._fileHandler.OpenConnection())
            {
                this._achievementCompletionRepository.InsertAsync(connection, new AchievementCompletion
                {
                    AchievementCompletionId = guidGenerator.GenerateGuid(),
                    AchievementId = @event.AggregateId,
                    Storypoints = @event.StoryPointsToAdd,
                    CounterIncreaseValue = @event.IncreaseCounterFor,
                    CompletedOn = @event.CompletedOn
                });
            }
        }

        #endregion

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