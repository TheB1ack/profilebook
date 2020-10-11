using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProfileBook.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        IAuthorizationService _authorizationService;
        IValidationService _validationService;

        private string _loginField;
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                _loginField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private string _passwordField;
        public string PasswordField
        {
            get { return _passwordField; }
            set
            {
                _passwordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private string _sPasswordField;
        public string SPasswordField
        {
            get { return _sPasswordField; }
            set
            {
                _sPasswordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private bool _isButtonEnable;
        public bool IsButtonEnable
        {
            get { return _isButtonEnable; }
            set
            {
                SetProperty(ref _isButtonEnable, value);
            }
        }
        public ICommand SingUpBClick => new Command(VerifyAndSave);

        public SingUpPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IValidationService validationService) : base(navigationService)
        {

            Title = "User SingUp";
            IsButtonEnable = false;
            _authorizationService = authorizationService;
            _validationService = validationService;
        }
        private bool TryActivateButton()
        {
            if (_loginField == null || _loginField == "")
            {
                return false;
            }
            if(_passwordField == null || _passwordField == "")
            {
                return false;
            }
            if(_sPasswordField == null || _sPasswordField == "")
            {
                return false;
            }
            return true;
        }
        private async void VerifyAndSave()
        {
            if (_validationService.VerifyInput(LoginField, PasswordField, SPasswordField))
            {
                _authorizationService.SingUp(_loginField, _passwordField);
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("login", _loginField);
                await NavigationService.GoBackAsync(parameters);
            }           
        }
    }
}
