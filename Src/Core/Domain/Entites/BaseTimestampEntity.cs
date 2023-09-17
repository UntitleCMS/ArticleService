namespace Domain.Entites;

public class BaseTimestampEntity<TId> : BaseEntity<TId>
{
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime LastUpdated { get; set; }

}
