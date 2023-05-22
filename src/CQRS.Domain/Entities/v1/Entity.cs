using CQRS.Domain.Contracts.v1;

namespace CQRS.Domain.Entities.v1;

public class Entity : IEntity
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public bool Equals(IEntity? other)
    {
        if(other is null) return false;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}