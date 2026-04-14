using Avalonia.Controls;
using FireBank.Models;
using FireBank.Services;
using FireBank.ViewModels;

namespace FireBank;

public partial class NewBankAccount : Window
{
    public NewBankAccount(User user, AccountService accountService)
    {
        InitializeComponent();

        var vm = new NewBankAccountViewModel(accountService, user);
        DataContext = vm;

        vm.AccountCreated += () => Close();
        vm.CloseRequested += () => Close();
    }
}