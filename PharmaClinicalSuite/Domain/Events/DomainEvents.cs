using PharmaClinicalSuite.Models.Interfaces;

namespace PharmaClinicalSuite.Models.Events
{
    public static class DomainEvents
    {
        private static List<Delegate> _handler  = new();

        public static void Register<T> (Action<T> handler) where T : IDomainEvent
        {
            _handler.Add(handler);
        }

        public static void Raise<T> (T domainEvent) where T : IDomainEvent
        {
            foreach (var handler in _handler.OfType<Action<T>>())
            {
                    handler(domainEvent);
            }

        }

    }
}
