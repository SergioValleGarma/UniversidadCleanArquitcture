// Universidad.Domain/Common/EntityBase.cs
using Universidad.Domain.Events;

namespace Universidad.Domain.Common;

public abstract class EntityBase
{

    private readonly List<DomainEventBase> _domainEvents = new();

    [System.Text.Json.Serialization.JsonIgnore]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}