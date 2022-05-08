using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRetailStoreApi.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("ProductId")]
        [BsonRepresentation(BsonType.String)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int AvailableQuantity { get; set; }

    }
}
