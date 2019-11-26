using System;

namespace Usavc.Microservices.Common.Types
{
    public abstract class BaseEntity : IIdentifiable
    {
        public string Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; } 
        public DateTime UpdatedDate { get; protected set; }

        public BaseEntity(string id)
        {
            Id = id;
            CreatedDate = DateTime.UtcNow;
            SetUpdatedDate();
        }

        protected virtual void SetUpdatedDate()
            => UpdatedDate = DateTime.UtcNow;
    }
}