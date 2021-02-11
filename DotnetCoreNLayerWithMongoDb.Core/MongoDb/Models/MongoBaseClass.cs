using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DotnetCoreNLayerWithMongoDb.Core.MongoDb.Models
{
    public abstract class MongoBaseClass
    {
        [BsonRepresentation(BsonType.String)]
        [BsonId]
        [BsonElement(Order = 0)]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
