using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FireBank.Services;
using FireBank.ViewModels;
using FireBank.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FireBank
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            Services = services.BuildServiceProvider();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
            base.OnFrameworkInitializationCompleted();

        }
        private static string GetDbPath()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MyApp", "data.db");
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            return path;
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            var dbPath = GetDbPath();

            services.AddSingleton<IAccountService>(_ => new AccountService(dbPath));
            services.AddSingleton<ITransactionService>(_ => new TransactionService(dbPath));
            services.AddSingleton<IUserService>(_ => new UserService(dbPath));
            services.AddSingleton<NavigationService>();

            services.AddTransient<DashboardViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<NewTransactionViewModel>();
            services.AddTransient<NewBankAccountViewModel>();
        }
    }
}