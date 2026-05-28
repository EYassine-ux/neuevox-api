using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NeueVox.Model.NeuevoxModel.MongoDb;

public class Quote
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  //TODO: auto generate
  public string? Id { get; set; }

  [BsonElement("Text")]
  public string Text { get; set; } = null!;

  [BsonElement("Author")] public string? Author { get; set; } = null!;
}
