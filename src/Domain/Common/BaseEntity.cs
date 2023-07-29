using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common;

public abstract class BaseEntity<TypeId>
    where TypeId : IEquatable<TypeId>
{
    [BsonId]
    public virtual TypeId? ID { get; set; }
}
