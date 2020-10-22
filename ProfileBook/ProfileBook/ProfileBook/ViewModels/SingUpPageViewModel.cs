using Acr.UserDialogs;
using Plugin.Settings;
using Prism.Navigation;
using ProfileBook.Services.Authentication;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        IAuthorizationService _authorizationService;

        private string _loginField;
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                _loginField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private string _passwordField;
        public string PasswordField
        {
            get { return _passwordField; }
            set
            {
                _passwordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private string _sPasswordField;
        public string SPasswordField
        {
            get { return _sPasswordField; }
            set
            {
                _sPasswordField = value;
                IsButtonEnable = TryActivateButton();
            }
        }
        private bool _isButtonEnable;
        public bool IsButtonEnable
        {
            get { return _isButtonEnable; }
            set
            {
                SetProperty(ref _isButtonEnable, value);
            }
        }
        public ICommand SingUpBClick => new Command(VerifyAndSave);

        public SingUpPageViewModel(INavigationService navigationService, IAuthorizationService authorizationService) : base(navigationService)
        {

            IsButtonEnable = false;
            _authorizationService = authorizationService;
        }
        private bool TryActivateButton()
        {
            if (LoginField == null || LoginField.Replace(" ","") == "")
            {
                return false;
            }
            if(PasswordField == null || PasswordField.Replace(" ", "") == "")
            {
                return false;
            }
            if(SPasswordField == null || SPasswordField.Replace(" ","") == "")
            {
                return false;
            }
            return true;
        }
        private async void VerifyAndSave()
        {
            if (CheckUserInput())
            {
                bool result = await _authorizationService.SingUpAsync(_loginField, _passwordField);
                if (result)
                {  
                    NavigationParameters parameters = new NavigationParameters();
                    parameters.Add("login", _loginField);
                    await NavigationService.GoBackAsync(parameters);
                }
            }           
        }
        private bool CheckUserInput()
        {
            if (LoginField.Length <= 4 || LoginField.Length >= 16)
            {
                string text = Resources.Resource.SingUpPage_AlertLoginLength;
                UserDialogs.Instance.Alert(text, "", "OK");
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(LoginField[0].ToString(), @"^[0-9]+"))
            {
                string text = Resources.Resource.SingUpPage_AlertLoginNumbers;
                UserDialogs.Instance.Alert(text, "", "OK");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(PasswordField, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])") || PasswordField.Length <= 8 || PasswordField.Length >= 16)
            {
                string text = Resources.Resource.SingUpPage_AlertPasswordValid;
                UserDialogs.Instance.Alert(text, "", "OK");
                return false;
            }
            if (PasswordField != SPasswordField)
            {
                string text = Resources.Resource.SingUpPage_AlertPasswordMatch;
                UserDialogs.Instance.Alert(text, "", "OK");
                return false;
            }
            return true;
        }
        private void ChangeLocalization(int localization)
        {
            switch (localization)
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
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            int localization = CrossSettings.Current.GetValueOrDefault("Localization", 0);
            ChangeLocalization(localization);
        }
    }
}
