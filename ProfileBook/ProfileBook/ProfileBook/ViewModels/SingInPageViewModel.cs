using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
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
        private string _loginField;
        private string _passwordField;
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                SetProperty(ref _loginField, value);
            }
        }
        public string PasswordField
        {
            get { return _passwordField; }
            set
            {
                SetProperty(ref _passwordField, value);
            }
        }
        public ICommand TapCommand => new Command<string>((url) => NavigationService.NavigateAsync(url));

        public SingInPageViewModel(INavigationService navigationService, IPageDialogService pageDialog) : base(navigationService)
        {
            Title = "User SingIn";
            _pageDialog = pageDialog;
            //alert();
        }

        private void alert(string message = "alert")
        {
            _pageDialog.DisplayAlertAsync("", message, "OK");
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoginField = (string)parameters["login"];
        }
    }
}
