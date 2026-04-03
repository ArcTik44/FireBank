using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FireBank.Services
{
    public class NavigationService
    {
        private readonly IServiceProvider _provider;
        private Window? _currentWindow;

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void SetCurrentWindow(Window window)
        {
            _currentWindow = window;
        }

        public void NavigateTo<TWindow, TViewModel>()
            where TWindow : Window, new()
            where TViewModel : class
        {
            var newWindow = new TWindow
            {
                DataContext = _provider.GetRequiredService<TViewModel>()
            };
            newWindow.Show();
            _currentWindow?.Close();
            _currentWindow = newWindow;
        }
    }
}
