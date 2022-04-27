using System;

namespace EventBus.Message.Events
{
    public class IntergrationBaseEvent
    {
        public IntergrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        public IntergrationBaseEvent(Guid id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
