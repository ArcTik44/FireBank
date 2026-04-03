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

        [ObservableProperty]
        private ObservableCollection<Account> accounts = [];

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions = [];

        public DashboardViewModel(IAccountService accountService, IUserService userService, ITransactionService transactionService)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.transactionService = transactionService;
            LoadTransactions();
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            accounts = new ObservableCollection<Account>();
        }

        private void LoadTransactions() {
            transactions = new ObservableCollection<Transaction>();
        }

        [RelayCommand]
        public void CreateAccount()
        {
            this.accountService.Insert(new Account
            {
                Id = ObjectId.NewObjectId(),
                AccountNumber = "1234567890",
                Balance = 1000,
                UserId = ObjectId.NewObjectId()
            }, ObjectId.NewObjectId());
        }

        [RelayCommand]
        public bool DeleteAccount() { 
            
        }

        [RelayCommand]
        public void CreateTransaction(string targetAccount, ObjectId srcUserId)
        {

            var targetAccount = accountService.GetAccountByAccountNumber(targetAccount);

            transactionService.Insert(new Transaction
            {
                Id = ObjectId.NewObjectId(),
                Amount = 100,
                Date = DateTime.Now,
                Note = "",
                FromAccountId = targetAccount.AccountNumber
            });
        }

    }
}
