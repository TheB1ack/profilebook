using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProfileBook.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {

        IPageDialogService _pageDialog;
        private string _loginField;
        private string _passwordField;
        private string _sPasswordField;
        private bool _isButtonEnable;
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                _loginField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        public string PasswordField
        {
            get { return _passwordField; }
            set
            {
                _passwordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        public string SPasswordField
        {
            get { return _sPasswordField; }
            set
            {
                _sPasswordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        public bool IsButtonEnable
        {
            get { return _isButtonEnable; }
            set
            {
                SetProperty(ref _isButtonEnable, value);
            }
        }

        public DelegateCommand SingUpBClick { get; }

        public SingUpPageViewModel(INavigationService navigationService, IPageDialogService pageDialog) : base(navigationService)
        {

            Title = "User SingUp";
            _pageDialog = pageDialog;
            SingUpBClick = new DelegateCommand(VerifyAndSave);
            IsButtonEnable = false;
        }
       /* private void alert(string message = "alert")
        {
            _pageDialog.DisplayAlertAsync("", message, "OK");
        }*/
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
            if (VerifyInput())
            {
                var parameters = new NavigationParameters();
                parameters.Add("login", _loginField);
                await NavigationService.NavigateAsync("SingInPage", parameters);
            }           
        }

        private bool VerifyInput()
        {
            if(_loginField.Length <=4 || _loginField.Length >= 16)
            {
                _pageDialog.DisplayAlertAsync("", "Login  must be at least 4 and no more than 16!", "OK");
                return false;
            }
            if(System.Text.RegularExpressions.Regex.IsMatch(_loginField[0].ToString(), @"^[0-9]+"))
            {
                _pageDialog.DisplayAlertAsync("", "Login musn't start with number!", "OK");
                return false;
            }
            //
            //проверка логина в бд
            //
            if (System.Text.RegularExpressions.Regex.IsMatch(_loginField[0].ToString(), @"^[0-9]+"))
            {
                
                _pageDialog.DisplayAlertAsync("", "This login is already taken!Make it unique!", "OK");
                return false;
            }
            if (_passwordField.Length <= 8 || _passwordField.Length >= 16)
            {
                _pageDialog.DisplayAlertAsync("", "Password must be at least 8 and no more than 16 and must contain at least one uppercase letter, one lowercase letter and one number!", "OK");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(_passwordField, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])"))
            {
                _pageDialog.DisplayAlertAsync("", "Password must be at least 8 and no more than 16 and must contain at least one uppercase letter, one lowercase letter and one number!", "OK");
                return false;
            }
            if (_passwordField != _sPasswordField)
            {
                _pageDialog.DisplayAlertAsync("", "Passwords must match!", "OK");
                return false;
            }
            return true;

        }
    }
}
