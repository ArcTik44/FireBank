using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FireBank.Services;
using FireBank.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace FireBank
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();
            services.AddSingleton<NavigationService>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<DashboardViewModel>();

            var provider = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = provider.GetRequiredService<MainWindowViewModel>()
                };
            }
        }
    }
}