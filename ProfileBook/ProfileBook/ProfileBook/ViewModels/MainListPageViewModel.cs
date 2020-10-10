using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        private string UserLogin;
        IAuthorizationService _authorizationService;

        public ICommand LogOutClick => new Command(LogOut);
        public ICommand SettingsClick => new Command(GoToSettings);
        public ICommand AddEditButtonClicked => new Command(GoToAddEditPage);
        public MainListPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService) : base(navigationService)
        {
            Title = "Main List";
            _authorizationService = authorizationService;
        }
        public async void GoToAddEditPage()
        {
            await NavigationService.NavigateAsync("AddEditProfilePage");
        }
        private async void LogOut()
        {
            _authorizationService.LogOut(UserLogin);
            await NavigationService.NavigateAsync("../SingInPage");
        }
        private async void GoToSettings()
        {
            await NavigationService.NavigateAsync("SettingsPage");
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UserLogin = App.Current.Properties["UserLogin"].ToString();
        }
    }
}
