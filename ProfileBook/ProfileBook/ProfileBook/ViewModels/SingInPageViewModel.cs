using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SingInPageViewModel : ViewModelBase
    {
        IPageDialogService _pageDialog;
        IAuthorizationService _authorizationService;

        private string _loginField;       
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                SetProperty(ref _loginField, value);
                IsButtonEnable = TryActivateButton();
            }
        }
        private string _passwordField;
        public string PasswordField
        {
            get { return _passwordField; }
            set
            {
                SetProperty(ref _passwordField, value);
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

        public ICommand SingInBClick => new Command(TryToSingIn);
        public ICommand TapCommand => new Command(NavigateToNextpageAsync);

        public SingInPageViewModel(INavigationService navigationService,IPageDialogService pageDialog,IAuthorizationService authorizationService) : base(navigationService)
        {
            Title = "User SingIn";
            _pageDialog = pageDialog;
            _authorizationService = authorizationService;
        }
        private async void TryToSingIn()
        {
            if (_authorizationService.SingIn(LoginField, PasswordField))
            {
                await NavigationService.NavigateAsync("../MainListPage");
            }
            else
            {
                await _pageDialog.DisplayAlertAsync("", "Invalid login or password!", "OK");
                PasswordField = "";
            }            
        }
        
        private bool TryActivateButton()
        {
            if (_passwordField == null || _passwordField == "")
            {
                return false;
            }
            return true;
        }
        private async void NavigateToNextpageAsync()
        {
            await NavigationService.NavigateAsync("SingUpPage");
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginField = (string)parameters["login"];
        }
    }
}
