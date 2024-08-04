using System;

namespace Library.Domain.Commom
{
    public abstract class Entity
    {
        protected Entity()
        {
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public long Id { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public void Stamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            Stamp();
        }

        public void Deactivate()
        {
            IsActive = false;
            Stamp();
        }
    }
}
