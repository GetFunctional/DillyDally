using System;

namespace GF.DillyDally.WriteModel.Games.Games
{
    internal class GameDetailsEntity
    {
        public GameDetailsEntity(Guid gameDetailsId, string title, string description, int rating, DateTime? releaseDate)
        {
            this.GameDetailsId = gameDetailsId;
            this.Title = title;
            this.Description = description;
            this.Rating = rating;
            this.ReleaseDate = releaseDate;
        }


        public Guid GameDetailsId { get; }

        public string Description { get; }

        public string Title { get; }

        public DateTime? ReleaseDate { get; }

        public int Rating { get; }
    }
}