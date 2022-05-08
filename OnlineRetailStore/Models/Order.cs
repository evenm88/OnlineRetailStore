using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineRetailStoreApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("OrderId")]
        [BsonRepresentation(BsonType.String)]
        public string OrderId { get; set; }
        [BsonElement("ProductId")]
        [BsonRepresentation(BsonType.String)]
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public double BillAmount { get; set; }
    }
}
