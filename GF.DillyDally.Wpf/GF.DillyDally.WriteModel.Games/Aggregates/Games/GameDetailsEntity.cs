using System;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Games
{
    internal class GameDetailsEntity
    {
        public GameDetailsEntity(Guid gameId, string title, string description, int? rating, DateTime? releaseDate)
        {
            this.GameId = gameId;
            this.Title = title;
            this.Description = description;
            this.Rating = rating;
            this.ReleaseDate = releaseDate;
        }


        public Guid GameId { get; }

        public string Description { get; }

        public string Title { get; }

        public DateTime? ReleaseDate { get; }

        public int? Rating { get; }
    }
}