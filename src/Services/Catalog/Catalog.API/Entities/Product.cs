using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]//MongoDb Id
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")] //Used to change the name of property inside of MongoDb
        public string Name { get; set; }

        public string Category { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
