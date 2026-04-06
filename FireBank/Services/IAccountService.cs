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
        bool DepositBalance(ObjectId accountId, decimal amount);
        Account? GetAccountByAccountNumber(string accountNumber);
        bool WithdrawBalance(ObjectId accountId, decimal amount);
        void Insert(Account account);
    }
}
