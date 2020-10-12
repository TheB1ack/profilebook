using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Profile;
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
        IAuthorizationService _authorizationService;
        IProfileService _profileService;
        IPageDialogService _pageDialog;

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
        public MainListPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAuthorizationService authorizationService, IProfileService profileService) : base(navigationService)
        {
            Title = "Main List";
            _authorizationService = authorizationService;
            _profileService = profileService;
            _pageDialog = pageDialog;
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
            User user = (User)App.Current.Properties["User"];
            _authorizationService.LogOut(user.UserLogin);
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
            //ListItems = null;
            User user = (User)App.Current.Properties["User"];
            ListItems = await _profileService.GetListOfContacts(user.UserId);
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
