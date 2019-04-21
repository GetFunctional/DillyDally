﻿namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IEventHandler<in TEvent> where TEvent : IAggregateEvent
    {
        void Handle(TEvent @event);
    }
}