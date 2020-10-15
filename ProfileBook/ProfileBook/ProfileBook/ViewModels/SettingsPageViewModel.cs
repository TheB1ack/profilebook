using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Enums;
using Plugin.Settings;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private SortEnum sortType;

        private bool _isCheckedName;
        public bool IsCheckedName
        {
            get { return _isCheckedName; }
            set
            {
                SetProperty(ref _isCheckedName, value);
            }
        }
        private bool _isCheckedNick;
        public bool IsCheckedNick
        {
            get { return _isCheckedNick; }
            set
            {
                SetProperty(ref _isCheckedNick, value);
            }
        }
        private bool _isCheckedDate;
        public bool IsCheckedDate
        {
            get { return _isCheckedDate; }
            set
            {
                SetProperty(ref _isCheckedDate, value);
            }
        }
        public ICommand RadioButtonChange => new Command(OnRadioButtonChange);
        public ICommand SaveBClick => new Command(SaveSettings);
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Settings";
        }
        private async void SaveSettings()
        {
            CrossSettings.Current.AddOrUpdateValue("Sort", (int)sortType);
            await NavigationService.GoBackAsync();
        }

        private void OnRadioButtonChange()
        {
            if (_isCheckedName)
            {
                sortType = SortEnum.ByName;
            }
            else if (_isCheckedNick)
            {
                sortType = SortEnum.ByNick;
            }
            else if (_isCheckedDate)
            {
                sortType = SortEnum.ByDate;
            }
        }
        private void ChangeRadioButton()
        {
            switch ((int)sortType)
            {
                case 0:
                    {
                        IsCheckedName = true;
                        break;
                    }
                case 1:
                    {
                        IsCheckedNick = true;
                        break;
                    }
                case 2:
                    {
                        IsCheckedDate = true;
                        break;
                    }
                default: break;
            }
                
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            sortType = (SortEnum)CrossSettings.Current.GetValueOrDefault("Sort", 0);
            ChangeRadioButton();
        }
    }
}
