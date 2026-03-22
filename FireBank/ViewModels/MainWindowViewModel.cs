using FireBank.Services;
using FireBank.Views;
using GalaSoft.MvvmLight;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FireBank.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        public ICommand GoToLoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToLoginCommand = ReactiveCommand.Create(GoToLogin);
            GoToRegisterCommand = ReactiveCommand.Create(GoToRegister);
        }

        private void GoToRegister()
        {
            _navigationService.NavigateTo<Register,RegisterViewModel>();
        }
        private void GoToLogin()
        {
            _navigationService.NavigateTo<Login,LoginViewModel>();
        }
    }
}
