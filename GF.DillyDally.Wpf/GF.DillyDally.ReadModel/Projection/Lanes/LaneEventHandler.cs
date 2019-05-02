﻿using System.Threading;
using System.Threading.Tasks;
using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using MediatR;

namespace GF.DillyDally.ReadModel.Projection.Lanes
{
    internal sealed class LaneEventHandler : INotificationHandler<LaneCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;

        public LaneEventHandler(DatabaseFileHandler fileHandler)
        {
            this._fileHandler = fileHandler;
        }

        #region INotificationHandler<LaneCreatedEvent> Members

        public async Task Handle(LaneCreatedEvent notification, CancellationToken cancellationToken)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                var laneRepository = new LaneRepository();
                await laneRepository.InsertAsync(connection, new LaneEntity
                {
                    LaneId = notification.AggregateId,
                    Name = notification.Name,
                    IsCompletedLane = notification.IsCompletedLane,
                    IsRejectedLane = notification.IsRejectedLane,
                    ColorCode = notification.ColorCode,
                    RunningNumberId = notification.RunningNumberId
                });
            }
        }

        #endregion
    }
}