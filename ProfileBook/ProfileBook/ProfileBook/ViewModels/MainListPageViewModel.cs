using Acr.UserDialogs;
using Plugin.Settings;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Profile;
using ProfileBook.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Theme;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        IAuthorizationService _authorizationService;
        IProfileService _profileService;
        IPopupNavigation _popupNavigation;

        private List<Contact> _listItems;
        public List<Contact> ListItems
        {
            get { return _listItems; }
            set
            {
                SetProperty(ref _listItems, value);
            }
        }
        private bool _isVisibleText;
        public bool IsVisibleText
        {
            get { return _isVisibleText; }
            set
            {
                SetProperty(ref _isVisibleText, value);
            }
        }
        private Contact _itemSelected;
        public Contact ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                _itemSelected = value;
                EnlargeImage();                       
            }
        }
        public ICommand EditTap => new Command(GoToAddEditPage);
        public ICommand DeleteTap => new Command(TryToDeleteContact);
        public ICommand LogOutClick => new Command(LogOut);
        public ICommand SettingsClick => new Command(GoToSettings);
        public ICommand AddEditButtonClicked => new Command(GoToAddEditPage);
        public MainListPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService, IPopupNavigation popupNavigation) : base(navigationService)
        {
            _authorizationService = authorizationService;
            _profileService = profileService;
            _popupNavigation = popupNavigation;
        }
        private async void EnlargeImage()
        {
            var page = (PopupPage)new ImagePopupPage();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                _popupNavigation.PopAsync(true);
            };
            var image = new Image() { Source = $"{ItemSelected.ImageSource}" };
            image.GestureRecognizers.Add(tapGestureRecognizer);
            page.Content = new StackLayout()
            {
                Children = { image },
                Padding = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            
            await _popupNavigation.PushAsync(page, true);
        }
        private async void GoToAddEditPage(object item = null)
        {
            Contact selectedContact = item as Contact;
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("contact", selectedContact);
            await NavigationService.NavigateAsync("AddEditProfilePage", parameters);
        }
        private async void LogOut()
        {
            _authorizationService.LogOut();
            await NavigationService.NavigateAsync("../SingInPage");
        }
        private async void TryToDeleteContact(object item)
        {
            string title = Resources.Resource.MainListPage_AlertTitle;
            string cancel = Resources.Resource.MainListPage_AlertCancel;
            string delete = Resources.Resource.MainListPage_AlertOk;
            Contact contact = item as Contact;
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
                .SetTitle(title + " " + contact.NickName +" ?")
                .SetCancelText(cancel)
                .SetOkText(delete));
            if (result)
            {
                int userId = CrossSettings.Current.GetValueOrDefault("UserId", -1);
                ListItems = await _profileService.RemoveContactAsync(contact, userId, (SortEnum)CrossSettings.Current.GetValueOrDefault("Sort", 0)); //как-то криво
                CheckListIsEmpty();
            }
        }
        private async void GoToSettings()
        {
            await NavigationService.NavigateAsync("../SettingsPage");
        }
        private void CheckListIsEmpty()
        {
            if (ListItems.Count == 0)
            {
                IsVisibleText = true;
            }
            else
            {
                IsVisibleText = false;
            }
        }
        private async void TryFillTheList()
        {
            int userId = CrossSettings.Current.GetValueOrDefault("UserId", -1);
            ListItems = await _profileService.GetListOfContacts(userId, (SortEnum)CrossSettings.Current.GetValueOrDefault("Sort", 0));           
            CheckListIsEmpty();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {  
            TryFillTheList();
        }
    }
}
