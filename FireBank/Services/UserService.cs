using FireBank.Models;
using Isopoh.Cryptography.Argon2;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace FireBank.Services
{
    public class UserService : IUserService
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<User> _collection;

        public UserService(LiteDatabase db)
        {
            _db = db;
            _collection = _db.GetCollection<User>("users");
        }

        public bool EmailExists(string email)
        {
            return _collection.Exists(u => u.Email == email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _collection.FindAll();
        }

        public User? GetUser(ObjectId userId)
        {
            return _collection.Find(user => user.Id == userId).FirstOrDefault();
        }

        public void Insert(User user,string password_plain)
        {
            user.PasswordHash = Argon2.Hash(password_plain);
            _collection.Insert(user);
        }

        public User ? Login(string email, string password)
        {
            var user = _collection.FindOne(u => u.Email == email);
            if (user == null || !Argon2.Verify(user.PasswordHash, password))
            {
                return null;
            }
            return user;
        }

        public bool UpdatePassword(ObjectId userId, string newPassword)
        {
            var user = _collection.FindOne(u => u.Id == userId);
            if(user == null)
                return false;
           _collection.Update(userId, new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                PasswordHash = Argon2.Hash(newPassword)
            });
            return true;
        }
    }
}
