using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Repository;
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
        IRepository<User> _repository;
        IAuthenticationService _authenticationService;
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

        public DelegateCommand SingInBClick { get; }
        public ICommand TapCommand => new Command(NavigateToNextpageAsync);

        public SingInPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAuthenticationService authenticationService, IAuthorizationService authorizationService) : base(navigationService)
        {
            Title = "User SingIn";
            _pageDialog = pageDialog;
            SingInBClick = new DelegateCommand(TryToSingIn);
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            //_repository = repository;
            //TryToSingInWhenInitiolize();
        }
        private async void TryToSingIn()
        {
            if(_repository == null)
            {
                _repository = new Repository();
            }
            if (_authenticationService.SingIn(LoginField, PasswordField, _repository))
            {
                User user = _authorizationService.UserAuthorization(LoginField, _repository);
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("user", user);
                parameters.Add("repository", _repository);
                await NavigationService.NavigateAsync("../MainListPage", parameters);
            }
            else
            {
                await _pageDialog.DisplayAlertAsync("", "Invalid login or password!", "OK");
                PasswordField = "";
            }            
        }
        //private async void TryToSingInWhenInitiolize()
        //{
        //    var items = _repository.GetItemsAsync().Result; //waiting 
        //    var user = from u
        //               in items
        //               where u.IsLoggedIn == true
        //               select u;
        //    if(user.FirstOrDefault() != null)
        //    {
        //        NavigationParameters parameters = new NavigationParameters();
        //        parameters.Add("user", user);
        //        parameters.Add("repository", _repository);
        //        await NavigationService.NavigateAsync("/MainListPage", parameters);
        //    }
        //}
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
            //NavigationParameters parameters = new NavigationParameters();
            //parameters.Add("repository", _repository);
            await NavigationService.NavigateAsync("SingUpPage");
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginField = (string)parameters["login"];
            _repository = (IRepository<User>)parameters["repository"];
        }
    }
}
