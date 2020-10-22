using Prism;
using Prism.Ioc;
using ProfileBook.ViewModels;
using ProfileBook.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using ProfileBook.Services.Repository;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Profile;
using Xamarin.Forms.Internals;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System.Globalization;

namespace ProfileBook
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            SetLocalization();
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

                        CultureInfo.CurrentUICulture = new CultureInfo("en", false);
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
    }
}
