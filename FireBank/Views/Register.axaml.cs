using Avalonia.Controls;
using FireBank.ViewModels;
using FireBank.Views;
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
        {
            new Login().Show();
            Close();
        };

        vm.GoToLoginRequested += () =>
        {
            new Login().Show();
            Close();
        };
    }
}