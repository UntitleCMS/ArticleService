namespace Domain.Entites;

public  class BaseEntity<TId>
{
    public virtual TId ID { get; set; }
}
