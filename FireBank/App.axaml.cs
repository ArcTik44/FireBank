using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FireBank.Services;
using FireBank.ViewModels;
using LiteDB;
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

            services.AddSingleton<LiteDatabase>(_ => new LiteDatabase(dbPath));
            services.AddSingleton<IAccountService>(sp => new AccountService(sp.GetRequiredService<LiteDatabase>()));
            services.AddSingleton<ITransactionService>(sp => new TransactionService(sp.GetRequiredService<LiteDatabase>()));
            services.AddSingleton<IUserService>(sp => new UserService(sp.GetRequiredService<LiteDatabase>()));
            services.AddSingleton<SessionService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
        }
    }
}