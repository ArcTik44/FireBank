using Avalonia.Controls;
using FireBank.Models;
using FireBank.Services;
using FireBank.ViewModels;

namespace FireBank;

public partial class NewTransaction : Window
{
    public NewTransaction(User user, AccountService accountService, TransactionService transactionService)
    {
        InitializeComponent();

        var vm = new NewTransactionViewModel(user, accountService, transactionService);
        DataContext = vm;

        vm.TransactionCreated += () => Close();
        vm.CloseRequested += () => Close();
    }
}