using FireBank.Services;
using FireBank.Views;
using GalaSoft.MvvmLight;
using ReactiveUI;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly NavigationService _navigationService;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(IUserService userService, NavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            LoginCommand = ReactiveCommand.Create(DoLogin);
            GoToRegisterCommand = ReactiveCommand.Create(() => _navigationService.NavigateTo<Register, RegisterViewModel>());
        }

        private void DoLogin()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Zadejte uživatelské jméno a heslo.";
                return;
            }

            var user = _userService.Login(Email, Password);
            if (user is null)
            {
                ErrorMessage = "Nesprávné uživatelské jméno nebo heslo.";
                return;
            }

            _navigationService.NavigateTo<Dashboard, DashboardViewModel>();
        }
    }
}