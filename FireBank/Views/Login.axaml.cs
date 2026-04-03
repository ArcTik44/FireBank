using Avalonia.Controls;

namespace FireBank.Views;

public partial class Login : Window
{

    public Login()
    {
        InitializeComponent();
    }

    private void GoToLogin_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (UsernameTextBox.Text == "admin" && PasswordTextBox.Text == "admin") {
            new Dashboard().Show();
            Close();
        }
        else
        {
            var dialog = new Window
            {
                Title = "Login Failed",
                Content = new TextBlock { Text = "Invalid username or password." },
                Width = 300,
                Height = 150
            };
            dialog.ShowDialog(this);
        }
        
    }

    private void GoToRegister_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        new Register().Show();
        Close();
    }
}