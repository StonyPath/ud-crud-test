namespace Domain.SeedWork;

public abstract class DomainEventBase
{
    public Guid Id { get; }
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    protected DomainEventBase() => Id = Guid.NewGuid();
}
