
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Api.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Info { get; set; }

    }
}
