using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using FireBank.Models;
using FireBank.Services;
using FireBank.ViewModels;

namespace FireBank;

public partial class NewTransaction : Window
{
    private NewTransactionViewModel? _vm;

    public NewTransaction(User user, AccountService accountService, TransactionService transactionService)
    {
        InitializeComponent();

        _vm = new NewTransactionViewModel(user, accountService, transactionService);
        DataContext = _vm;

        _vm.TransactionCreated += () => Close();
        _vm.CloseRequested += () => Close();
    }

    private async void PasteAccountNumber_Click(object? sender, RoutedEventArgs e)
    {
        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard is null || _vm is null)
            return;

        try
        {
            var text = await clipboard.TryGetTextAsync();
            if (!string.IsNullOrWhiteSpace(text))
                _vm.ToAccountNumber = text.Trim();
        }
        catch
        {
            // ignore clipboard errors
        }
    }
}
