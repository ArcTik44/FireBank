using FireBank.Models;
using FireBank.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public class NewBankAccountViewModel : ViewModelBase
    {
        public event Action? AccountCreated;
        public event Action? CloseRequested;

        private readonly AccountService _accountService;
        private readonly AccountNumberGenerator _accountNumberGenerator;
        private readonly User _user;

        private string _errorMessage = string.Empty;

        public ICommand CancelCommand { get; }
        public ICommand CreateAccountCommand { get; }

        public ObservableCollection<Currency> Currencies { get; } = [];
        public ObservableCollection<AccountType> AccountTypes { get; } = [];

        public Currency SelectedCurrency { get; set; }
        public AccountType SelectedAccountType { get; set; }
        public decimal InitialBalance { get; set; }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public NewBankAccountViewModel(AccountService accountService, User user)
        {
            _accountService = accountService;
            _accountNumberGenerator = new AccountNumberGenerator();
            _user = user;

            CancelCommand = ReactiveCommand.Create(() => CloseRequested?.Invoke());
            CreateAccountCommand = ReactiveCommand.Create(DoCreateAccount);

            foreach (var c in Enum.GetValues<Currency>())
                Currencies.Add(c);
            foreach (var t in Enum.GetValues<AccountType>())
                AccountTypes.Add(t);

            SelectedCurrency = Currency.CZK;
            SelectedAccountType = AccountType.Běžný;
        }

        private void DoCreateAccount()
        {
            ErrorMessage = string.Empty;

            if (InitialBalance < 0)
            {
                ErrorMessage = "Počáteční zůstatek nemůže být záporný.";
                return;
            }

            var account = new Account
            {
                UserId = _user.Id,
                AccountNumber = _accountNumberGenerator.GenerateNational(),
                AccountType = SelectedAccountType,
                Currency = SelectedCurrency,
                Balance = InitialBalance
            };

            _accountService.Insert(account);
            AccountCreated?.Invoke();
        }
    }
}
