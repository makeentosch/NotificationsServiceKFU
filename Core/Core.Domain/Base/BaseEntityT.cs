using Core.Domain.Abstractions;

namespace Core.Domain.Base;

public abstract class BaseEntity<TId> : BaseEntity, IEntity<TId>
{
    public TId Id { get; set; }

    protected BaseEntity()
    {
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }
}