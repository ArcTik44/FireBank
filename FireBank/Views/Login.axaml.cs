using Avalonia.Controls;
using Avalonia.Threading;
using FireBank.Services;
using FireBank.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FireBank.Views;

public partial class Login : Window
{
    public Login()
    {
        InitializeComponent();

        var vm = App.Services.GetRequiredService<LoginViewModel>();
        DataContext = vm;

        vm.LoginSuccessful += () =>
            Dispatcher.UIThread.Post(() =>
            {
                var session = App.Services.GetRequiredService<SessionService>();
                var dashboard = new Dashboard(session.CurrentUser!);
                dashboard.Show();
                Close();
            });

        vm.GoToRegisterRequested += () =>
            Dispatcher.UIThread.Post(() =>
            {
                new Register().Show();
                Close();
            });
    }
}