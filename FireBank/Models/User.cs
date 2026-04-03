using LiteDB;

namespace FireBank.Models;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; } = ObjectId.NewObjectId();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";
}
