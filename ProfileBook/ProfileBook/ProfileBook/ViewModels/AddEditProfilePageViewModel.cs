using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfilePageViewModel : ViewModelBase
    {
        IProfileService _profileService;

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
                SetProperty(ref _nameField, value); ;
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
        public Action CameraAction;
        public Action GalleryAction;
        public ICommand SaveClick => new Command(SaveContact);
        public ICommand TapCommand => new Command(GetNewPhoto);
        public AddEditProfilePageViewModel(INavigationService navigationService, IProfileService profileService) : base(navigationService)
        {
            Title = "Add Profile";
            _profileService = profileService;
            GalleryAction += TakePhotoFromGallery;
            CameraAction += TakePhotoWithCamera;
        }

        public void GetNewPhoto()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                .SetTitle("Choose photo")
                .Add("Pick at Gallery", GalleryAction, "ic_collections.png")
                .Add("Take photo with camera", CameraAction, "ic_camera_alt.png"));
        }
        private async void TakePhotoFromGallery()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo == null)
                {
                    return;
                }
                ImageSource = photo.Path;
            }
        }
        private async void TakePhotoWithCamera()
        {
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
                    return;
                }
                ImageSource = photo.Path;
            }
        }
        private async void SaveContact()
        {
            if (NameField == null || NickField == null || NameField.Replace(" ", "") == "" || NickField.Replace(" ", "") == "")
            {
               await UserDialogs.Instance.AlertAsync("Fields Name and NickName musn't be empty!");
            }
            else
            {
                User user = (User)App.Current.Properties["User"];
                _profileService.AddOrEditContact(NameField ?? "", NickField ?? "", DescriptionField ?? "", ImageSource, user.UserId, _contact);
                await NavigationService.GoBackAsync();
            }
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
