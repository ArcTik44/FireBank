using FireBank.Models;
using FireBank.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public class NewTransactionViewModel : ViewModelBase
    {
        public event Action? TransactionCreated;
        public event Action? CloseRequested;

        private readonly User _user;
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;

        private string _note = string.Empty;
        private string _toAccountNumber = string.Empty;
        private decimal _amount = Decimal.Zero;
        private string _errorMessage = string.Empty;
        private Account? _selectedFromAccount;

        public ICommand CancelCommand { get; }
        public ICommand SendCommand { get; }

        public ObservableCollection<Account> UserAccounts { get; } = [];

        public Account? SelectedFromAccount
        {
            get => _selectedFromAccount;
            set
            {
                if (SetProperty(ref _selectedFromAccount, value))
                    OnSelectedAccountChanged();
            }
        }

        public string Note { get => _note; set => SetProperty(ref _note, value); }
        public string ToAccountNumber { get => _toAccountNumber; set => SetProperty(ref _toAccountNumber, value); }
        public decimal Amount { get => _amount; set => SetProperty(ref _amount, value); }
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

        public string AvailableBalance => SelectedFromAccount is not null
            ? $"{SelectedFromAccount.Balance:N2} {SelectedFromAccount.Currency}"
            : string.Empty;

        public string DisplayName => _user.FullName;

        public NewTransactionViewModel(User user, AccountService accountService, TransactionService transactionService)
        {
            _user = user;
            _accountService = accountService;
            _transactionService = transactionService;

            CancelCommand = new RelayCommand(() => CloseRequested?.Invoke());
            SendCommand = new RelayCommand(DoSendTransaction);

            foreach (var acc in _accountService.GetAccountsByUserId(_user.Id))
                UserAccounts.Add(acc);

            if (UserAccounts.Count > 0)
                SelectedFromAccount = UserAccounts[0];
        }

        private void OnSelectedAccountChanged()
        {
            OnPropertyChanged(nameof(AvailableBalance));
        }

        private void DoSendTransaction()
        {
            ErrorMessage = string.Empty;

            // Validace: vybrán zdrojový účet
            if (SelectedFromAccount is null)
            {
                ErrorMessage = "Vyberte účet, ze kterého chcete odeslat.";
                return;
            }

            // Validace: číslo cílového účtu
            if (string.IsNullOrWhiteSpace(ToAccountNumber))
            {
                ErrorMessage = "Zadejte číslo cílového účtu.";
                return;
            }

            // Validace: částka
            if (Amount <= 0)
            {
                ErrorMessage = "Zadejte platnou kladnou částku.";
                return;
            }

            // Validace: dostatek prostředků
            if (Amount > SelectedFromAccount.Balance)
            {
                ErrorMessage = "Nedostatek prostředků na účtu.";
                return;
            }

            // Vyhledání cílového účtu
            var toAccount = _accountService.GetAccountByAccountNumber(ToAccountNumber.Trim());
            if (toAccount is null)
            {
                ErrorMessage = "Cílový účet nebyl nalezen.";
                return;
            }

            // Nelze poslat sám sobě
            if (toAccount.Id == SelectedFromAccount.Id)
            {
                ErrorMessage = "Nelze odeslat na stejný účet.";
                return;
            }

            // Provedení transakce: odečtení + připsání
            if (!_accountService.WithdrawBalance(SelectedFromAccount.Id, Amount))
            {
                ErrorMessage = "Odeslání se nezdařilo.";
                return;
            }

            _accountService.DepositBalance(toAccount.Id, Amount);

            // Záznam transakce
            var transaction = new Transaction
            {
                FromAccountId = SelectedFromAccount.Id,
                ToAccountId = toAccount.Id,
                Amount = Amount,
                Currency = SelectedFromAccount.Currency,
                Note = Note,
                Date = DateTime.Now
            };

            _transactionService.Insert(transaction);

            TransactionCreated?.Invoke();
        }
    }
}
