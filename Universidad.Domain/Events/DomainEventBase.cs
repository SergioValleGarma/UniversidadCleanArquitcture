// Universidad.Domain/Events/DomainEventBase.cs
namespace Universidad.Domain.Events;

public abstract class DomainEventBase
{
    public Guid EventId { get; }
    public DateTime OccurredOn { get; }

    protected DomainEventBase()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}
