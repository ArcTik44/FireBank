using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    internal class TransactionService : ITransactionService, IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Transaction> _collection;

        public TransactionService(string databasePath)
        {
            _db = new LiteDatabase(databasePath);
            _collection = _db.GetCollection<Transaction>("transactions");
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionByUserId(ObjectId userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(ObjectId accountId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
