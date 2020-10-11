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
using ProfileBook.Services.Validator;
using ProfileBook.Services.Profile;
using Xamarin.Forms.Internals;

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
            //if (CrossAutoLogin.Current.UserIsSaved)
            //{
            //    await NavigationService.NavigateAsync("NavigationPage/MainListPage");
            //}
            //else
            //{
            await NavigationService.NavigateAsync("NavigationPage/SingInPage");
            //}
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SingInPage, SingInPageViewModel>();
            containerRegistry.RegisterForNavigation<SingUpPage, SingUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfilePageViewModel>();

            containerRegistry.Register<IRepository<User>, Repository<User>>();
            containerRegistry.Register<IRepository<Contact>, Repository<Contact>>();
            containerRegistry.Register<IAuthorizationService, AuthorizationService>();
            containerRegistry.Register<IValidationService, ValidationService>();
            containerRegistry.Register<IProfileService, ProfileService>();

        }
    }
}
