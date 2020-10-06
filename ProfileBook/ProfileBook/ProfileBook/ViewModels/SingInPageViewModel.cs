using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SingInPageViewModel : ViewModelBase
    {
        public ICommand TapCommand => new Command<string>((url) => NavigationService.NavigateAsync(url));

        public SingInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "User SingIn";
        }
    }
}
