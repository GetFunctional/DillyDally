using Dapper;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : IEventHandler<AchievementCreatedEvent>,
        IEventHandler<AchievementCompletedEvent>, IEventHandler<AchievementCounterValueChangedEvent>
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

        public async void Handle(AchievementCompletedEvent @event)
        {
            var guidGenerator = this._fileHandler.GuidGenerator;

            using (var connection = this._fileHandler.OpenConnection())
            {
                await this._achievementCompletionRepository.InsertAsync(connection, new AchievementCompletion
                                                                                    {
                                                                                        AchievementCompletionId = guidGenerator.GenerateGuid(),
                                                                                        AchievementId = @event.AggregateId,
                                                                                        Storypoints = @event.StoryPointsToAdd,
                                                                                        CounterIncreaseValue = @event.IncreaseCounterFor,
                                                                                        CompletedOn = @event.CompletedOn
                                                                                    }).ConfigureAwait(false);
            }
        }

        #endregion

        #region IEventHandler<AchievementCounterValueChangedEvent> Members

        public async void Handle(AchievementCounterValueChangedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementToChange = await this._repository.GetByIdAsync(@event.AggregateId);
                achievementToChange.CounterIncrease = @event.NewCounterValue;
                await connection.ExecuteAsync($"UPDATE {AchievementEntity.TableNameConstant} SET {nameof(AchievementEntity.CounterIncrease)} = @counterIncrease;",
                    new {counterIncrease = @event.NewCounterValue});
            }
        }

        #endregion

        #region IEventHandler<AchievementCreatedEvent> Members

        public async void Handle(AchievementCreatedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                await this._repository.InsertAsync(connection, new AchievementEntity
                                                               {
                                                                   AchievementId = @event.AggregateId,
                                                                   Name = @event.Name,
                                                                   CounterIncrease = @event.CounterIncrease,
                                                                   StoryPoints = @event.Storypoints,
                                                                   RunningNumberId = @event.RunningNumberId
                                                               }).ConfigureAwait(false);
            }
        }

        #endregion
    }
}