using System;
using GF.DillyDally.WriteModel.Infrastructure;

namespace GF.DillyDally.WriteModel.Domain.Tasks.Events
{
    public sealed class DefinitionOfDoneChangedEvent : AggregateEventBase
    {
        public DefinitionOfDoneChangedEvent(Guid aggregateId, string definitionOfDone) : base(aggregateId)
        {
            this.DefinitionOfDone = definitionOfDone;
        }

        public string DefinitionOfDone { get; }
    }
}