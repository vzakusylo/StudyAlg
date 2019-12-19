using MediatR;
using System;
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

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        internal void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }
    }
    // Вместо немедленной отправки обработчику событий рекомендуется добавить события предметной 
    // области в коллекцию, а затем отправить их непосредственно до или непосредственно после фиксации 
    // транзакции(как в случае с SaveChanges в EF). 
}
