namespace GF.DillyDally.WriteModel.Infrastructure
{
    public interface IEventHandler<in TEvent> where TEvent : IAggregateEvent
    {
        void Handle(TEvent @event);
    }
}