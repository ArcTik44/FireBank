using FireBank.Models;
using FireBank.Services;
using FireBank.Views;
using GalaSoft.MvvmLight;
using ReactiveUI;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly NavigationService _navigationService;

        private bool _acceptTerms;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        private string _errorMessage = string.Empty;

        public bool AcceptTerms
        {
            get => _acceptTerms;
            set => Set(ref _acceptTerms, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => Set(ref _confirmPassword, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        public RegisterViewModel(IUserService userService, NavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;
            RegisterCommand = ReactiveCommand.Create(DoRegister);
            GoToLoginCommand = ReactiveCommand.Create(() => _navigationService.NavigateTo<Login, LoginViewModel>());
        }

        private void DoRegister()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                ErrorMessage = "Zadejte jméno a příjmení.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@'))
            {
                ErrorMessage = "Zadejte platný e-mail.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                ErrorMessage = "Heslo musí mít alespoň 6 znaků.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Hesla se neshodují.";
                return;
            }

            if (_userService.EmailExists(Email))
            {
                ErrorMessage = "Tento e-mail je již registrován.";
                return;
            }


            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
            };
            _userService.Insert(newUser,Password);
            _navigationService.NavigateTo<Dashboard, DashboardViewModel>();
        }
    }
}