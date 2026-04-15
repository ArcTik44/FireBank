using Avalonia.Controls;
using Avalonia.Threading;
using FireBank.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FireBank.Views;

public partial class Register : Window
{
    public Register()
    {
        InitializeComponent();

        var vm = App.Services.GetRequiredService<RegisterViewModel>();
        DataContext = vm;

        vm.RegisterSuccessful += () =>
            Dispatcher.UIThread.Post(() =>
            {
                new Login().Show();
                Close();
            });

        vm.GoToLoginRequested += () =>
            Dispatcher.UIThread.Post(() =>
            {
                new Login().Show();
                Close();
            });
    }
}