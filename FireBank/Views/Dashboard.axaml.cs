using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FireBank.Services;
using FireBank.ViewModels;

namespace FireBank;

public partial class Dashboard : Window
{
    public Dashboard(DashboardViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Dashboard():this (new DashboardViewModel(new AccountService("firebank.db"),
        new UserService("firebank.db"), 
        new TransactionService("firebank.db"))){ }
    }