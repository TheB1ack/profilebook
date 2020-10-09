using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
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
        private User _user;
        private IRepository<User> _repository;
        //public ICommand LogOutClick => new Command(LogOut);
        public MainListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Main List";
        }
        private async void LogOut()
        {
            //_repository.UpdateItemLogged(_user.UserLogin, false);
            await NavigationService.NavigateAsync("/SingInPage");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _user = (User)parameters["user"];
            _repository = (IRepository<User>)parameters["repository"];
        }
    }
}
