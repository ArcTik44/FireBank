using CommunityToolkit.Mvvm.ComponentModel;
using FireBank.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FireBank.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private UserService userService;

        public LoginViewModel(UserService userService)
        {
            this.userService = userService;
        }
    }
}
