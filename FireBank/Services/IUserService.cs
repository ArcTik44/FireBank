using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetByUserId(ObjectId id);
        User ? GetByEmail(string email);
        void Insert(User user);
        bool Update(User user);
        bool Delete(ObjectId id);
    }
}
