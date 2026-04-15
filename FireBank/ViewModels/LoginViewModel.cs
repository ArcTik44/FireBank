using FireBank.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly SessionService _sessionService;


        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;

        public event Action? LoginSuccessful;
        public event Action? GoToRegisterRequested;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(IUserService userService, SessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
            LoginCommand = new RelayCommand(DoLogin);
            LoginSuccessful += () => { 
                
            };
            GoToRegisterRequested += () => { 
            };

            GoToRegisterCommand = new RelayCommand(
                () => GoToRegisterRequested?.Invoke());
        }

        private void DoLogin()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            { ErrorMessage = "Zadejte e-mail a heslo."; return; }

            var user = _userService.Login(Email, Password);
            if (user is null) { ErrorMessage = "Nesprávné přihlašovací údaje."; return; }

            _sessionService.CurrentUser = user;
            LoginSuccessful?.Invoke();
        }
    }
}