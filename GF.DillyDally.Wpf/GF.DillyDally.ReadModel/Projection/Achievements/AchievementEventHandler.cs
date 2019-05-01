using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : INotificationHandler<AchievementCreatedEvent>,
        INotificationHandler<AchievementCompletedEvent>, INotificationHandler<AchievementCounterValueChangedEvent>
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

        #region INotificationHandler<AchievementCompletedEvent> Members

        public async Task Handle(AchievementCompletedEvent notification, CancellationToken cancellationToken)
        {
            var guidGenerator = this._fileHandler.GuidGenerator;

            using (var connection = this._fileHandler.OpenConnection())
            {
                await this._achievementCompletionRepository.InsertAsync(connection, new AchievementCompletion
                {
                    AchievementCompletionId = guidGenerator.GenerateGuid(),
                    AchievementId = notification.AggregateId,
                    Storypoints = notification.StoryPointsToAdd,
                    CounterIncreaseValue = notification.IncreaseCounterFor,
                    CompletedOn = notification.CompletedOn
                }).ConfigureAwait(false);
            }
        }

        #endregion

        #region INotificationHandler<AchievementCounterValueChangedEvent> Members

        public async Task Handle(AchievementCounterValueChangedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementToChange = await this._repository.GetByIdAsync(notification.AggregateId);
                achievementToChange.CounterIncrease = notification.NewCounterValue;
                await connection.ExecuteAsync(
                    $"UPDATE {AchievementEntity.TableNameConstant} SET {nameof(AchievementEntity.CounterIncrease)} = @counterIncrease;",
                    new {counterIncrease = notification.NewCounterValue});
            }
        }

        #endregion

        #region INotificationHandler<AchievementCreatedEvent> Members

        public async Task Handle(AchievementCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                await this._repository.InsertAsync(connection, new AchievementEntity
                {
                    AchievementId = notification.AggregateId,
                    Name = notification.Name,
                    CounterIncrease = notification.CounterIncrease,
                    StoryPoints = notification.Storypoints,
                    RunningNumberId = notification.RunningNumberId
                }).ConfigureAwait(false);
            }
        }

        #endregion
    }
}