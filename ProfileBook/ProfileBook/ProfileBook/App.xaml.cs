using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Forms;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Profile;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System.Globalization;
using System.Collections.Generic;
using ProfileBook.Theme;

namespace ProfileBook
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            SetLocalization();
        }

        protected override async void OnInitialized()
        {
            
            InitializeComponent();

            SetLocalization();
            ChangeTheme();
            if (CrossSettings.Current.GetValueOrDefault("UserId", -1) != -1)
            {
                await NavigationService.NavigateAsync("NavigationPage/MainListPage");                                              
            }
            else
            {
                await NavigationService.NavigateAsync("NavigationPage/SingInPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SingInPage, SingInPageViewModel>();
            containerRegistry.RegisterForNavigation<SingUpPage, SingUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ImagePopupPage, ImagePopupPageViewModel>();

            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterInstance<IPopupNavigation>(PopupNavigation.Instance);

            containerRegistry.RegisterInstance<IRepository<User>>(Container.Resolve<Repository<User>>());
            containerRegistry.RegisterInstance<IRepository<Contact>>(Container.Resolve<Repository<Contact>>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            
        }
        private void SetLocalization()
        {
            int selectedLocalization = CrossSettings.Current.GetValueOrDefault("Localization", 0);
            switch (selectedLocalization)
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
            int selectedTheme = CrossSettings.Current.GetValueOrDefault("Theme", 0);
            switch (selectedTheme)
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
    }
}
