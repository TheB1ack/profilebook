using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        public SingUpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "User SingUp";
        }

        private void VerifyAndSave(object sender, System.EventArgs e)
        {
            //Button button = (Button)sender;
            //button.Text = "Нажато!";
            //button.BackgroundColor = Color.Red;
            VerifyInput();
        }

        private bool VerifyInput()
        {

            //проверка логина с бд

            return true;

        }
    }
}
