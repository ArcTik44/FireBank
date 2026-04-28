using Avalonia.Controls;
using Avalonia.Threading;
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
            Dispatcher.UIThread.Post(() =>
            {
                var window = new NewTransaction(user, accountService, transactionService);
                window.Closed += (_, _) => { 
                    vm.RefreshTransactions();
                    vm.RefreshAccounts();
                };
                window.ShowDialog(this);
            });

        vm.GoToNewAccountRequested += () =>
            Dispatcher.UIThread.Post(() =>
            {
                var window = new NewBankAccount(user, accountService);
                window.Closed += (_, _) => vm.RefreshAccounts();
                window.ShowDialog(this);
            });

        vm.LogoutRequested += () =>
            Dispatcher.UIThread.Post(() =>
            {
                new Login().Show();
                Close();
            });
    }
}