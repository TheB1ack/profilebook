using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace ProfileBook.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        private DelegateCommand SingUpButtonClick { get; }
        IPageDialogService _pageDialog;
        private string _loginField;
        public string LoginField
        {
            get { return _loginField; }
            set
            {
                _loginField = value;              
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}
        public SingUpPageViewModel(INavigationService navigationService, IPageDialogService pageDialog) : base(navigationService)
        {
            Title = "User SingUp";
            _pageDialog = pageDialog;
            SingUpButtonClick = new DelegateCommand(VerifyAndSave);
        }
        

        private void VerifyAndSave()
        {
            //Button button = (Button)sender;
            //button.Text = "Нажато!";
            //button.BackgroundColor = Color.Red;
            _pageDialog.DisplayAlertAsync("Alert", "message", "Ok");
            VerifyInput();
            
        }

        private bool VerifyInput()
        {
            //проверка логина с бд
            if (_loginField != "")
            {
                return false;
            }
            return true;

        }
    }
}
