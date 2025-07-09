namespace Domain.SeedWork;

public abstract class Entity<TId> : IEntity
{
    public TId Id { get; protected set; }
    private List<DomainEventBase> _domainEvents = [];
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents?.AsReadOnly();

    protected Entity() => _domainEvents = new List<DomainEventBase>();

    public void AddDomainEvent(DomainEventBase eventItem) => _domainEvents.Add(eventItem);

    public void ClearDomainEvents() => _domainEvents?.Clear();
}