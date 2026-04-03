using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccountsByUserId(ObjectId userId);
        Account ? GetAccountByAccountId(ObjectId accountId);

        Account ? GetAccountByAccountNumber(string accountNumber);

        void Insert(Account account, ObjectId userId);
        bool Delete(ObjectId accountId);
    }
}
