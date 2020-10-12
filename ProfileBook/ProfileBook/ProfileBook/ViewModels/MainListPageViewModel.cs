using Acr.UserDialogs;
using Plugin.Settings;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Profile;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : ViewModelBase
    {
        IAuthorizationService _authorizationService;
        IProfileService _profileService;

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

        public ICommand EditTap => new Command(GoToAddEditPage);
        public ICommand DeleteTap => new Command(TryToDeleteContact);
        public ICommand LogOutClick => new Command(LogOut);
        public ICommand SettingsClick => new Command(GoToSettings);
        public ICommand AddEditButtonClicked => new Command(GoToAddEditPage);
        public MainListPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService, IProfileService profileService) : base(navigationService)
        {
            Title = "Main List";
            _authorizationService = authorizationService;
            _profileService = profileService;
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
            Contact contact = item as Contact;
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig()
                .SetTitle($"Delete contact {contact.NickName} ?")
                .SetCancelText("Cancel")
                .SetOkText("Delete"));
            if (result)
            {
                _profileService.RemoveContact(contact);
                TryFillTheList();
            }
        }
        private async void GoToSettings()
        {
            await NavigationService.NavigateAsync("SettingsPage");
        }
        private async void TryFillTheList()
        {
            int userId = CrossSettings.Current.GetValueOrDefault("UserId", -1);
            ListItems = await _profileService.GetListOfContacts(userId);
            if(ListItems.Count == 0)
            {
                IsVisibleText = true;
            }
            else
            {
                IsVisibleText = false;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {  
            TryFillTheList();
        }
    }
}
