using LiteDB;
using System;
namespace FireBank.Models;

public class Transaction
{
    [BsonId]
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();
    public ObjectId FromAccountId { get; set; } = ObjectId.Empty;
    public ObjectId ToAccountId { get; set; } = ObjectId.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public Currency Currency { get; set; }

    [BsonIgnore]
    public string FromAccountNumber { get; set; } = string.Empty;
    [BsonIgnore]
    public string ToAccountNumber { get; set; } = string.Empty;
}
