using Avalonia.Controls;
using FireBank.ViewModels;
using FireBank.Views;
using Microsoft.Extensions.DependencyInjection;


namespace FireBank
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToLogin_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var vm = App.Services.GetRequiredService<LoginViewModel>();
            new Login(){ DataContext = vm}.Show();
            Close();
        }

        private void GoToRegister_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var vm = App.Services.GetRequiredService<RegisterViewModel>();
            new Register(){ DataContext = vm}.Show();
            Close();
        }
    }
}