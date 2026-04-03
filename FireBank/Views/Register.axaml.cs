using Avalonia.Controls;

namespace FireBank.Views;

public partial class Register : Window
{
    public Register()
    {
        InitializeComponent();
    }

    private void GoToLogin_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new Login().Show();
        Close();
    }
}