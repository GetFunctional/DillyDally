using System;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Games.Events
{
    public class GameCreated : AggregateEventBase
    {
        public GameCreated(Guid aggregateId, string title, string description, int? rating, DateTime? releaseDate) :
            base(aggregateId)
        {
            this.Title = title;
            this.Description = description;
            this.Rating = rating;
            this.ReleaseDate = releaseDate;
        }

        public string Title { get; }
        public string Description { get; }
        public int? Rating { get; }
        public DateTime? ReleaseDate { get; }
    }
}