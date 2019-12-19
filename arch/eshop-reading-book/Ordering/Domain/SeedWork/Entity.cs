using MediatR;
using System.Collections.Generic;

namespace Ordering.Domain.SeedWork
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        int _Id;

        public virtual int Id
        {
            get { return _Id; }
            protected set { _Id = value; }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvent => _domainEvents.AsReadOnly();
    }
}
