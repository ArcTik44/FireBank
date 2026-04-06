using FireBank.Models;
using FireBank.Services;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LiteDB;
using System;

namespace FireBank.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly ITransactionService transactionService;
        private User user;
        private AccountNumberGenerator accountNumberGenerator;

        [ObservableProperty]
        private ObservableCollection<Account> accounts;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        public DashboardViewModel(IAccountService accountService, IUserService userService, ITransactionService transactionService)
        {
            this.accountNumberGenerator = new AccountNumberGenerator();
            this.accountService = accountService;
            this.userService = userService;
            this.transactionService = transactionService;
            LoadTransactions();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            accounts = (ObservableCollection<Account>)this.accountService.GetAccountsByUserId(this.user.Id);
        }

        private void LoadTransactions() {
            foreach (var account in accounts)
            {
              foreach (var transaction in this.transactionService.GetTransactionsByAccountId(account.Id))
              {
                transactions.Add(transaction);
              }
            }
            
        }

        [RelayCommand]
        public void CreateAccount(ObjectId userId)
        {
            
            this.accountService.Insert(new Account
            {
                Id = ObjectId.NewObjectId(),
                AccountNumber = accountNumberGenerator.GenerateNational(),
                Balance = 0,
                
                UserId = userId
            });
        }

        [RelayCommand]
        public void CreateTransaction(string targetAccountNumber, string srcAccountNumber,
            decimal amount, string note, Currency currency)
        {

            var srcAccount = accountService.GetAccountByAccountNumber(srcAccountNumber);
            var tgtAccount = accountService.GetAccountByAccountNumber(targetAccountNumber);
            if (srcAccount != null && tgtAccount != null)
            {
                if (tgtAccount.Currency != srcAccount.Currency)
                {
                    throw new InvalidOperationException("Účty musí mít stejnou měnu");
                }

                this.transactionService.Insert(new Transaction
                {
                    Id = ObjectId.NewObjectId(),
                    FromAccountId = srcAccount.Id,
                    ToAccountId = tgtAccount.Id,
                    Note = note,
                    Amount = amount,
                    Currency = currency,
                });
            }    
           
        }

    }
}
