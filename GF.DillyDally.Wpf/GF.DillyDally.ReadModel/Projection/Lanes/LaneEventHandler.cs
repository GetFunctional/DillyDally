using GF.DillyDally.Data.Sqlite;
using GF.DillyDally.ReadModel.Repository;
using GF.DillyDally.ReadModel.Repository.Entities;
using GF.DillyDally.WriteModel.Domain.Lanes.Events;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.ReadModel.Projection.Lanes
{
    internal sealed class LaneEventHandler : IEventHandler<LaneCreatedEvent>
    {
        private readonly DatabaseFileHandler _fileHandler;
        private readonly ILaneRepository _laneRepository;

        public LaneEventHandler(DatabaseFileHandler fileHandler, ILaneRepository laneRepository)
        {
            this._fileHandler = fileHandler;
            this._laneRepository = laneRepository;
        }

        #region IEventHandler<LaneCreatedEvent> Members

        public void Handle(LaneCreatedEvent @event)
        {
            using (var connection = this._fileHandler.OpenConnection())
            {
                this._laneRepository.InsertAsync(connection, new LaneEntity
                                                             {
                                                                 LaneId = @event.AggregateId,
                                                                 Name = @event.Name,
                                                                 IsCompletedLane = @event.IsCompletedLane,
                                                                 IsRejectedLane = @event.IsRejectedLane,
                                                                 ColorCode = @event.ColorCode,
                                                                 RunningNumberId = @event.RunningNumberId
                                                             });
            }
        }

        #endregion
    }
}