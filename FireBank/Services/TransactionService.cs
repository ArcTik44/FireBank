using FireBank.Models;
using LiteDB;
using System.Collections.Generic;

namespace FireBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Transaction> _collection;

        public TransactionService(LiteDatabase db)
        {
            _db = db;
            _collection = _db.GetCollection<Transaction>("transactions");
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
