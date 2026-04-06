using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _db.Dispose();
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(ObjectId accountId)
        {
            return _collection.Query().Where(tr=>tr.FromAccountId.Equals(accountId)).ToList();
        }

        public void Insert(Transaction transaction)
        {
            _collection.Insert(transaction);
        }
    }
}
