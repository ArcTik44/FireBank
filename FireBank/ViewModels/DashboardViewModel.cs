using FireBank.Models;
using FireBank.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;
        private readonly User _user;

        public event Action? GoToNewTransactionRequested;
        public event Action? GoToNewAccountRequested;
        public event Action? LogoutRequested;

        public ICommand LogoutCommand { get; }
        public ICommand NewTransactionCommand { get; }
        public ICommand NewAccountCommand { get; }

        public string WelcomeMessage => $"Vítejte, {_user.FullName}";
        public string SelectedAccount => Accounts.FirstOrDefault()?.AccountNumber ?? "Žádný účet";

        public ObservableCollection<Account> Accounts { get; } = [];
        public ObservableCollection<Transaction> Transactions { get; } = [];

        public DashboardViewModel(
            AccountService accountService,
            TransactionService transactionService,
            User user)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _user = user;

            NewAccountCommand = new RelayCommand(() => GoToNewAccountRequested?.Invoke());
            NewTransactionCommand = new RelayCommand(() => GoToNewTransactionRequested?.Invoke());
            LogoutCommand = new RelayCommand(() => LogoutRequested?.Invoke());

            RefreshAccounts();
            RefreshTransactions();
        }

        public void RefreshTransactions()
        {
            Transactions.Clear();
            foreach (var acc in Accounts)
            {
                foreach (var tr in _transactionService.GetTransactionsByAccountId(acc.Id))
                {
                    tr.FromAccountNumber = acc.AccountNumber;
                    Transactions.Add(tr);
                }
            }
        }

        public void RefreshAccounts()
        {
            Accounts.Clear();
            foreach (var acc in _accountService.GetAccountsByUserId(_user.Id))
                Accounts.Add(acc);

            Transactions.Clear();
            foreach (var acc in Accounts)
            {
                foreach (var tr in _transactionService.GetTransactionsByAccountId(acc.Id))
                {
                    tr.FromAccountNumber = acc.AccountNumber;
                    Transactions.Add(tr);
                }
            }
        }
    }
}