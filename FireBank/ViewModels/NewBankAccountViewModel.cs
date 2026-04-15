using FireBank.Models;
using FireBank.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public class NewBankAccountViewModel : ViewModelBase
    {
        public event Action? AccountCreated;
        public event Action? CloseRequested;
        private AccountService accountService;
        private User user;

        public ICommand CancelCommand {  get; set; }
        public ICommand CreateAccountCommand { get; set; }
        public ObservableCollection<Currency> Currencies { get; set; } = [];
        public ObservableCollection<AccountType> AccountTypes { get; set; } = [];
        public Currency SelectedCurrency { get; set; }
        public AccountType SelectedAccountType { get; set; }

        public decimal InitialBalance { get; set; }
        public string ErrorMessage { get; set; }


        public NewBankAccountViewModel(AccountService accountService, User user)
        {
            this.accountService = accountService;
            this.user = user;
        }
    }
}