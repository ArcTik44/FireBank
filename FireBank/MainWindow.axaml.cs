using Avalonia.Controls;
using FireBank.Views;

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
            new Login().Show();
            Close();
        }

        private void GoToRegister_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            new Register().Show();
            Close();
        }
    }
}
