using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FireBank.Services;
using FireBank.ViewModels;
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
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
            services.AddSingleton<DatabaseService>();
            services.AddSingleton<UserService>();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Cesta k databázi v AppData
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MyApp", "data.db");

            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

            // Singleton – sdílíme jednu instanci databáze
            services.AddSingleton<IAccountService>(_ => new AccountService(dbPath));
            services.AddSingleton<ITransactionService>(_ => new TransactionService(dbPath));

            // ViewModely a okna
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<NewTransactionViewModel>();
            services.AddTransient<NewBankAccountViewModel>();
            services.AddTransient<MainWindow>();
        }
    }
}