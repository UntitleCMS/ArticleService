
namespace Domain.Entites;

public class BaseAuditableEntity<TId,TAuditId> : BaseTimestampEntity<TId>
{
    public virtual TAuditId AuthorId { get; set; }
}
