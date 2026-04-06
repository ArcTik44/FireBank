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

        public Account ? GetAccountByAccountNumber(string accountNumber)
        {
            return _collection.Query().Where(acc => acc.AccountNumber == accountNumber).FirstOrDefault();
        }

        public IEnumerable<Account> GetAccountsByUserId(ObjectId userId)
        {
            return _collection.Query().Where(acc=>acc.UserId == userId).ToList();
        }

        public void Insert(Account account)
        {
            _collection.Insert(account);
        }

        public bool DepositBalance(ObjectId accountId, decimal amount)
        {
            Account updatedAcc = _collection.FindById(accountId);
            if (updatedAcc == null)
            {
                return false;
            }
            updatedAcc.Balance += amount;
            _collection.Update(updatedAcc);
            return true;
        }

        public bool WithdrawBalance(ObjectId accountId, decimal amount)
        {
            Account updatedAcc = _collection.FindById(accountId);
            if (updatedAcc == null || updatedAcc.Balance < amount)
            {
                return false;
            }
            updatedAcc.Balance -= amount;
            _collection.Update(updatedAcc);
            return true;
        }
    }
}
