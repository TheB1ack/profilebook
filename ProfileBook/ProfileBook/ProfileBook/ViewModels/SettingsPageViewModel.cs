using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using ProfileBook.Enums;
using Plugin.Settings;
using System.Globalization;
using System.Collections.Generic;
using ProfileBook.Theme;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private SortEnum selectedSort;
        private ThemeEnum selectedTheme;
        private LocalizationEnum selectedLocalization;

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
                OnCheckboxChange();
            }
        }
        private string _pickerItem;
        public string PickerItem
        {
            get { return _pickerItem; }
            set
            {
                SetProperty(ref _pickerItem, value);
                OnPickerChange();
            }
        }
        public ICommand RadioButtonChanged => new Command(OnRadioButtonChange);
        public ICommand SaveBClick => new Command(SaveSettings);
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        private async void SaveSettings()
        {
            CrossSettings.Current.AddOrUpdateValue("Sort", (int)selectedSort);
            CrossSettings.Current.AddOrUpdateValue("Theme", (int)selectedTheme);
            CrossSettings.Current.AddOrUpdateValue("Localization", (int)selectedLocalization);
            ChangeLocalization((int)selectedLocalization);
            ChangeTheme();
            await NavigationService.NavigateAsync("../MainListPage");
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
        private void OnPickerChange()
        {
            switch (PickerItem)
            {
                case "English":
                    {
                        selectedLocalization = LocalizationEnum.English;
                        break;
                    }
                case "Русский":
                    {
                        selectedLocalization = LocalizationEnum.Russian;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void OnCheckboxChange()
        {
            if (IsCheckedDark)
            {
                selectedTheme = ThemeEnum.Dark;
            }
            else
            {
                selectedTheme = ThemeEnum.Default;
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
                default: 
                    {
                        break; 
                    }
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
                default:
                    {
                        break;
                    }
            }
        }
        private void ChangePicker()
        {
            switch ((int)selectedLocalization)
            {
                case 0:
                    {
                        PickerItem = "English";
                        break;
                    }
                case 1:
                    {
                        PickerItem = "Русский";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void ChangeLocalization(int localization)
        {
            switch (localization)
            {
                case 0:
                    {
                        CultureInfo.CurrentUICulture = new CultureInfo("en-US", false);
                        break;
                    }
                case 1:
                    {
                        CultureInfo.CurrentUICulture = new CultureInfo("ru", false);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void ChangeTheme()
        {
            switch ((int)selectedTheme)
            {
                case 0:
                    {
                        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                        if (mergedDictionaries != null)
                        {
                            mergedDictionaries.Clear();
                            mergedDictionaries.Add(new LightTheme());
                        }
                        break;
                    }
                case 1:
                    {
                        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                        if (mergedDictionaries != null)
                        {
                            mergedDictionaries.Clear();
                            mergedDictionaries.Add(new DarkTheme());
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            selectedSort = (SortEnum)CrossSettings.Current.GetValueOrDefault("Sort", 0);
            ChangeRadioButton();
            selectedTheme = (ThemeEnum)CrossSettings.Current.GetValueOrDefault("Theme", 0);
            ChangeCheckBox();
            selectedLocalization = (LocalizationEnum)CrossSettings.Current.GetValueOrDefault("Localization", 0);
            ChangePicker();
        }
    }
}
