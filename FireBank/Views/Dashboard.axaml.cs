using Avalonia.Controls;
using FireBank.Models;
using FireBank.Services;
using FireBank.ViewModels;
using FireBank.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FireBank;

public partial class Dashboard : Window
{
    public Dashboard(User user)
    {
        InitializeComponent();

        var accountService = (AccountService)App.Services.GetRequiredService<IAccountService>();
        var transactionService = (TransactionService)App.Services.GetRequiredService<ITransactionService>();

        var vm = new DashboardViewModel(accountService, transactionService, user);
        DataContext = vm;

        vm.GoToNewTransactionRequested += () =>
        {
            var window = new NewTransaction(user, accountService, transactionService);
            window.ShowDialog(this); // dialog nad dashboardem, nezavírá ho
        };

        vm.GoToNewAccountRequested += () =>
        {
            var window = new NewBankAccount(user, accountService);
            window.Closed += (_, _) => vm.RefreshAccounts(); // po zavření obnov účty
            window.ShowDialog(this);
        };

        vm.LogoutRequested += () =>
        {
            new Login().Show();
            Close();
        };
    }
}