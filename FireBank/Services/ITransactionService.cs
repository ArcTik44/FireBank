using FireBank.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactionsByAccountId(ObjectId accountId);
        IEnumerable<Transaction> GetTransactionsByUserId(ObjectId userId);
        void Insert(Transaction transaction);

    }
}
