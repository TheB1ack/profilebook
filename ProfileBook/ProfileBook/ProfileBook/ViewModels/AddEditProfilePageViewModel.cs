using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfilePageViewModel : ViewModelBase
    {
        IProfileService _profileService;
        IPageDialogService _pageDialog;

        private Contact _contact;

        
        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                SetProperty(ref _imageSource, value);
            }
        }
        private string _nickField;
        public string NickField
        {
            get { return _nickField; }
            set
            {
                SetProperty(ref _nickField, value);
            }
        }
        private string _nameField;
        public string NameField
        {
            get { return _nameField; }
            set
            {
                SetProperty(ref _nameField, value);;
            }
        }
        private string _descriptionField;
        public string DescriptionField
        {
            get { return _descriptionField; }
            set
            {
                SetProperty(ref _descriptionField, value);
            }
        }

        public ICommand SaveClick => new Command(SaveContact);
        public ICommand TapCommand => new Command(GetNewPhoto);
        public AddEditProfilePageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IProfileService profileService) : base(navigationService)
        {
            Title = "Add Profile";
            _profileService = profileService;
            _pageDialog = pageDialog;
        }

        public async void GetNewPhoto()
        {

            string actionSheet = await _pageDialog.DisplayActionSheetAsync("Choose photo", "Cancel", null, "Pick at Gallery", "Take photo with camera");  //для картинок юзать только пакет?

            switch (actionSheet)
            {
                case "Cancel":
                    break;

                case "Pick at Gallery":
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                        if (photo == null)
                        {
                            break;
                        }
                        ImageSource = photo.Path;
                    }
                    break;

                case "Take photo with camera":
                    if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            SaveToAlbum = true,
                            Directory = "Sample",
                            Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                        });

                        if (photo == null)
                        {
                            break;
                        }
                        ImageSource = photo.Path;
                    }
                    break;

                default:
                    break;
            }
        }
        private async void SaveContact()
        {
            User user = (User)App.Current.Properties["User"];    
            _profileService.AddOrEditContact(NameField ?? "", NickField ?? "", DescriptionField ?? "", ImageSource, user.UserId, _contact);
            await NavigationService.GoBackAsync();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _contact = (Contact)parameters["contact"];
            if (_contact != null)
            {
                NameField = _contact.FullName;
                NickField = _contact.NickName;
                ImageSource = _contact.ImageSource;
                DescriptionField = _contact.Description;
            }
            else
            {
                ImageSource = "pic_profile.png";
            }
        }
    }
}
