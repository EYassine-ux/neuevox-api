using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NeueVox.Model.NeuevoxModel.MongoDb;

public class Announcement
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  [BsonElement("Title")]
  public string Title { get; set; } = null!;

  [BsonElement("Message")]
  public string Message { get; set; } = null!;

  [BsonElement("DatePosted")]
  public DateTime DatePosted { get; set; }
}
