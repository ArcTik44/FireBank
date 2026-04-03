using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    internal class UserService : IUserService, IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<User> _collection;

        public UserService(string dbPath)
        {
            this._db = new LiteDatabase(dbPath);
            this._collection = _db.GetCollection<User>("users");
        }

        public bool Delete(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this._db.Dispose();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _collection.FindAll();
        }

        public User? GetByEmail(string email)
        {
            return _collection.FindOne(u => u.Email == email);
        }

        public User? GetByUserId(ObjectId id)
        {
            return _collection.FindOne(u => u.Id == id);
        }

        public void Insert(User user)
        {
            throw new NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
