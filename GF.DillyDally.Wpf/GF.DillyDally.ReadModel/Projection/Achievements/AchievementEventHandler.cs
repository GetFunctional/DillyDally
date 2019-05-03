using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Achievements.Repository;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : INotificationHandler<AchievementCreatedEvent>,
        INotificationHandler<AchievementCompletedEvent>, INotificationHandler<AchievementCounterValueChangedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public AchievementEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<AchievementCompletedEvent> Members

        public async Task Handle(AchievementCompletedEvent notification, CancellationToken cancellationToken)
        {
            var guidGenerator = this._fileHandler.GuidGenerator;

            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementCompletionRepository = new AchievementCompletionRepository();
                await achievementCompletionRepository.InsertAsync(connection, new AchievementCompletion
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
                var achievementRepository = new AchievementRepository();
                await achievementRepository.ChangeCounterValueAsync(connection, notification.AggregateId, notification.NewCounterValue);
            }
        }

        #endregion

        #region INotificationHandler<AchievementCreatedEvent> Members

        public async Task Handle(AchievementCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementRepository = new AchievementRepository();
                await achievementRepository.InsertAsync(connection, new AchievementEntity
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