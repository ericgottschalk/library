using System;

namespace Library.Domain.Commom
{
    public abstract class Entity
    {
        protected Entity()
        {
            IsActive = true;
        }

        public long Id { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public void Stamp()
        {
            UpdatedAt = DateTime.Now;
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
