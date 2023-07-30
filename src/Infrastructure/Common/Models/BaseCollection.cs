
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Common.Models;

public class BaseCollection<TID> where TID : IEquatable<TID>
{
    [BsonId]
    public TID? Id { get; set; }
}
