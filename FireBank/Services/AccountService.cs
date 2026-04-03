using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace FireBank.Services
{
    public class AccountService : IAccountService, IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Account> _collection;

        public AccountService(string dbPath)
        {
            _db = new LiteDatabase(dbPath);
            _collection = _db.GetCollection<Account>("accounts");
            _collection.EnsureIndex(accs => accs.Id);
        }

        public bool Delete(ObjectId accountId)
        {
            return _collection.Delete(accountId);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Account ? GetAccountByAccountId(ObjectId accountId)
        {
            return _collection.FindById(accountId);
        }

        public Account ? GetAccountByAccountNumber(string accountNumber)
        {
            return _collection.Query().Where(acc => acc.AccountNumber == accountNumber).FirstOrDefault();
        }

        public IEnumerable<Account> GetAccountsByUserId(ObjectId userId)
        {
            return _collection.Query().Where(acc=>acc.UserId == userId).ToList();
        }

        public void Insert(Account account, ObjectId userId)
        {
            account.UserId = userId;
            _collection.Insert(account);
        }
    }
}
