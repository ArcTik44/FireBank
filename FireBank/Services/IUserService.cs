using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    public interface IUserService
    {
        User? GetUser(ObjectId userId);
        void Insert(User user,string password_plain);
        bool UpdatePassword(ObjectId userId, string newPassword);
        bool Login(string email, string password);
    }
}
