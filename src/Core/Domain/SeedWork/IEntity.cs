
namespace Domain.SeedWork;

public interface IEntity
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }

    void AddDomainEvent(DomainEventBase eventItem);
    void ClearDomainEvents();
}