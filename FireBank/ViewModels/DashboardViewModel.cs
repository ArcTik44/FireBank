using FireBank.Models;
using FireBank.Services;
using ReactiveUI;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        private readonly NavigationService _navigationService;
        private readonly AccountNumberGenerator _accountNumberGenerator;

        public ICommand CreateAccountCommand { get; }
        public ICommand CreateTransaction { get; }

        public DashboardViewModel(AccountService accountService, UserService userService,
            TransactionService transactionService, NavigationService navigationService)
        {
            _accountNumberGenerator = new AccountNumberGenerator();
            _accountService = accountService;
            _navigationService = navigationService;
            _userService = userService;
            _transactionService = transactionService;

            CreateAccountCommand = ReactiveCommand.Create(() =>
                _navigationService.NavigateTo<NewBankAccount, NewBankAccountViewModel>());
            CreateTransaction = ReactiveCommand.Create(() =>
                _navigationService.NavigateTo<NewTransaction, NewTransactionViewModel>());
        }
    }
}