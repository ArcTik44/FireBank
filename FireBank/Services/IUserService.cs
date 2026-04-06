using FireBank.Models;
using LiteDB;


namespace FireBank.Services
{
    public interface IUserService
    {
        User? GetUser(ObjectId userId);
        void Insert(User user,string password_plain);
        bool UpdatePassword(ObjectId userId, string newPassword);
        bool EmailExists(string email);
        User? Login(string email, string password);
    }
}
