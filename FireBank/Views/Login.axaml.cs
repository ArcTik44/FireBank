using Avalonia.Controls;
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
        {
            var session = App.Services.GetRequiredService<SessionService>();
            var dashboard = new Dashboard(session.CurrentUser!);
            dashboard.Show();
            Close();
        };

        vm.GoToRegisterRequested += () =>
        {
            new Register().Show();
            Close();
        };
    }
}