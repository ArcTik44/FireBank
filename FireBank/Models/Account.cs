using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using LiteDB;

namespace FireBank.Models;

public enum AccountType
{
    Běžný,
    Spořící
}

public class Account
{
    [BsonId]
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();
    public ObjectId UserId { get; set; } = ObjectId.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public Currency Currency { get; set; } = Currency.CZK;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string DisplayName => $"{AccountNumber} ({AccountType}) – {Balance:N2} {Currency}";
}
