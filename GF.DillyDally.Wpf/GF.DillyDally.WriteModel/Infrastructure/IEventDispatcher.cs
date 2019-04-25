using System;

namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface IEventDispatcher
    {
        void RegisterHandler<TEvent>(IEventHandler<TEvent> handler) where TEvent : class, IAggregateEvent;
        bool HasHandlerForEvent(Type eventImplementation);
    }
}