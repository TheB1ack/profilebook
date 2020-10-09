using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfilePageViewModel : ViewModelBase
    {
        private Image _userImage;
        public Image UserImage
        {
            get { return _userImage; }
            set
            {
                SetProperty(ref _userImage, value);
            }
        }
        public AddEditProfilePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add/Edit Profile";
        }
        

        
    }
}
