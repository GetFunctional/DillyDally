using System;
using GF.DillyDally.WriteModel.Core.Aggregates;
using GF.DillyDally.WriteModel.Games.Aggregates.Games.Events;

namespace GF.DillyDally.WriteModel.Games.Aggregates.Games
{
    internal sealed class GameAggregate : AggregateRootBase
    {
        public GameAggregate()
        {
            this.RegisterTransition<GameCreated>(this.Apply);
            //this.RegisterTransition<TaskLinkCreatedEvent>(this.Apply);
            //this.RegisterTransition<AttachedFileToTaskEvent>(this.Apply);
            //this.RegisterTransition<PreviewImageAssignedEvent>(this.Apply);
            //this.RegisterTransition<DefinitionOfDoneChangedEvent>(this.Apply);
            //this.RegisterTransition<TaskLinkedToActivitiesEvent>(this.Apply);
        }

        private GameAggregate(Guid gameId, string title, string description, int? rating, DateTime? releaseDate)
        {
            this.AggregateId = gameId;
            var creationEvent = new GameCreated(gameId, title, description, rating, releaseDate);
            this.RaiseEvent(creationEvent);
        }

        private GameDetailsEntity GameDetails { get; set; }

        private void Apply(GameCreated obj)
        {
            this.AggregateId = obj.AggregateId;
            this.GameDetails =
                new GameDetailsEntity(obj.AggregateId, obj.Title, obj.Description, obj.Rating, obj.ReleaseDate);
        }
    }
}