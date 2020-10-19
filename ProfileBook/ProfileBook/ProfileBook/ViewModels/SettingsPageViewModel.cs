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
using Prism.Events;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private SortEnum selectedSort;
        private ThemeEnum selectedTheme;

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
        private bool _isCheckedDark;
        public bool IsCheckedDark
        {
            get { return _isCheckedDark; }
            set
            {
                SetProperty(ref _isCheckedDark, value);
                if(_isCheckedDark)
                {
                    selectedTheme = ThemeEnum.Dark;
                }
                else
                {
                    selectedTheme = ThemeEnum.Default;
                }
            }
        }
        private string _pickerItem;
        public string PickerItem
        {
            get { return _pickerItem; }
            set
            {
                SetProperty(ref _pickerItem, value);
            }
        }
        public ICommand RadioButtonChanged => new Command(OnRadioButtonChange);
        public ICommand SaveBClick => new Command(SaveSettings);
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Settings";
        }
        private async void SaveSettings()
        {
            CrossSettings.Current.AddOrUpdateValue("Sort", (int)selectedSort);
            CrossSettings.Current.AddOrUpdateValue("Theme", (int)selectedTheme);
            await NavigationService.GoBackAsync();
        }
        private void OnRadioButtonChange()
        {
            if (_isCheckedName)
            {
                selectedSort = SortEnum.ByName;
            }
            else if (_isCheckedNick)
            {
                selectedSort = SortEnum.ByNick;
            }
            else if (_isCheckedDate)
            {
                selectedSort = SortEnum.ByDate;
            }
        }
        private void ChangeRadioButton()
        {
            switch ((int)selectedSort)
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
        private void ChangeCheckBox()
        {
            switch ((int)selectedTheme)
            {
                case 0:
                    {
                        IsCheckedDark = false;
                        break;
                    }
                case 1:
                    {
                        IsCheckedDark = true;
                        break;
                    }
                default: break;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            selectedSort = (SortEnum)CrossSettings.Current.GetValueOrDefault("Sort", 0);
            ChangeRadioButton();
            selectedTheme = (ThemeEnum)CrossSettings.Current.GetValueOrDefault("Theme", 0);
            ChangeCheckBox();

        }
    }
}
