﻿using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Projection.Achievements.Repository;
using GF.DillyDally.WriteModel.Domain.Achievements.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Achievements
{
    internal sealed class AchievementEventHandler : INotificationHandler<AchievementCreatedEvent>,
        INotificationHandler<AchievementCompletedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public AchievementEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<AchievementCompletedEvent> Members

        public async Task Handle(AchievementCompletedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementCompletionRepository = new AchievementRepository();
                await achievementCompletionRepository.CompletedAsync(connection, notification.AggregateId, notification.CompletedOn);
            }
        }

        #endregion

        #region INotificationHandler<AchievementCreatedEvent> Members

        public async Task Handle(AchievementCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var achievementRepository = new AchievementRepository();
                await achievementRepository.CreateNewAsync(connection, notification.AggregateId, notification.Name, notification.RunningNumberId);
            }
        }

        #endregion
    }
}