namespace GF.DillyDally.WriteModel.Infrastructure
{
    internal interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}