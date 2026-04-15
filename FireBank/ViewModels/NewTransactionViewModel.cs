using CommunityToolkit.Mvvm.ComponentModel;
using FireBank.Models;
using FireBank.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public class NewTransactionViewModel : ViewModelBase
    {
        public event Action? TransactionCreated;
        public event Action? CloseRequested;
        private User user;
        private AccountService accountService;
        private TransactionService transactionService;

        public ICommand CancelCommand => ReactiveCommand.Create(() => CloseRequested?.Invoke());
        public ICommand SendCommand => ReactiveCommand.Create(DoSendTransaction);

        public ObservableCollection<Account> UserAccounts { get; } = [];

        public Account? SelectedFromAccount { get; set; }

        public string _note = string.Empty;
        public string _toAccountNumber = string.Empty;
        public string _availableBalance = string.Empty;
        public string _amount = string.Empty;
        public string _displayName = string.Empty;
        public string _errorMessage = string.Empty;


        public string Note { get => _note; set => SetProperty(ref _note, value); }
        public string ToAccountNumber { get => _toAccountNumber; set => SetProperty(ref _toAccountNumber, value); }
        public string AvailableBalance { get => _availableBalance; set => SetProperty(ref _availableBalance, value); }
        public string Amount { get => _amount; set => SetProperty(ref _amount, value); }
        public string DisplayName { get => _displayName; set => SetProperty(ref _displayName, value); }
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

        private void DoSendTransaction()
        {
            throw new NotImplementedException();
        }

        public NewTransactionViewModel(User user, AccountService accountService, TransactionService transactionService)
        {
            this.user = user;
            this.accountService = accountService;
            this.transactionService = transactionService;
        }
    }
}