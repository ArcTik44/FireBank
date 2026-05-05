using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FireBank.Models;
using FireBank.Services;
using FireBank.ViewModels;
using FireBank.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

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

    private async void CopyAccountNumber_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string accountNumber || string.IsNullOrWhiteSpace(accountNumber))
            return;

        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard is null)
            return;

        try
        {
            await clipboard.SetTextAsync(accountNumber);
            await ShowCopyStatus($"Zkopírováno: {accountNumber}");
        }
        catch
        {
            await ShowCopyStatus("Kopírování selhalo");
        }
    }

    private async Task ShowCopyStatus(string message)
    {
        var status = this.FindControl<TextBlock>("CopyStatus");
        if (status is null) return;

        status.Text = message;
        status.IsVisible = true;

        await Task.Delay(1800);

        if (status.Text == message)
        {
            status.IsVisible = false;
            status.Text = string.Empty;
        }
    }
}
