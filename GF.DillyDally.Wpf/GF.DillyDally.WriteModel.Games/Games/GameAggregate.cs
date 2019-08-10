using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.DillyDally.WriteModel.Core.Aggregates;

namespace GF.DillyDally.WriteModel.Games.Games
{
    internal class GameAggregate : AggregateRootBase
    {

        public GameAggregate()
        {
            //this.RegisterTransition<TaskCreatedEvent>(this.Apply);
            //this.RegisterTransition<TaskLinkCreatedEvent>(this.Apply);
            //this.RegisterTransition<AttachedFileToTaskEvent>(this.Apply);
            //this.RegisterTransition<PreviewImageAssignedEvent>(this.Apply);
            //this.RegisterTransition<DefinitionOfDoneChangedEvent>(this.Apply);
            //this.RegisterTransition<TaskLinkedToActivitiesEvent>(this.Apply);
        }

        private GameAggregate(Guid gameId, string name, Guid runningNumberId,
            Guid categoryId, Guid? previewImageFileId, int storypoints = 0)
        {
            var creationEvent = new GameCreatedEvent(taskId, name, runningNumberId, categoryId,
                previewImageFileId, DateTime.Now, storypoints);
            this.RaiseEvent(creationEvent);
        }

        private GameDetailsEntity GameDetails { get; }

    }
}
