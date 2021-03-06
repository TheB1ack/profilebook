﻿using Acr.UserDialogs;
using Plugin.Settings;
using Prism.Navigation;
using ProfileBook.Services.Authentication;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SingInPageViewModel : ViewModelBase
    {
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

        public ICommand SingInBClick => new Command(TryToSingInAsync);
        public ICommand TapCommand => new Command(NavigateToNextpageAsync);

        public SingInPageViewModel(INavigationService navigationService,IAuthorizationService authorizationService) : base(navigationService)
        {
            IsButtonEnable = false;
            _authorizationService = authorizationService;
        }
        private async void TryToSingInAsync()
        {
            bool isValid = await _authorizationService.SingInAsync(LoginField, PasswordField);
            if (isValid)
            {
                CrossSettings.Current.AddOrUpdateValue("Sort", 0);
                await NavigationService.NavigateAsync("../MainListPage");
            }
            else
            {
                string text = Resources.Resource.SingInPage_Alert;
                await UserDialogs.Instance.AlertAsync(text, "", "OK");
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
            PasswordField = "";
        }
    }
}
