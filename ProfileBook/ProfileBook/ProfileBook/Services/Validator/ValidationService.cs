using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.Authentication;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Validator
{
    public class ValidationService: IValidationService
    {
        private readonly IPageDialogService _pageDialog;
        private readonly IRepository<User> _repository;
        public ValidationService(IPageDialogService pageDialog, IRepository<User> repository)
        {
            _pageDialog = pageDialog;
            _repository = repository;
        }
        public bool VerifyInput(string login, string password, string spassword)
        {
            if (login.Length <= 4 || login.Length >= 16)
            {
                _pageDialog.DisplayAlertAsync("", "Login must be at least 4 and no more than 16!", "OK");
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(login[0].ToString(), @"^[0-9]+"))
            {
                _pageDialog.DisplayAlertAsync("", "Login musn't start with numbers!", "OK");
                return false;
            }
            if (_repository.GetItemByLogin(login) != null)
            {
                _pageDialog.DisplayAlertAsync("", "This login is already taken!", "OK");
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])") || password.Length <= 8 || password.Length >= 16)
            {
                _pageDialog.DisplayAlertAsync("", "Password must be at least 8 and no more than 16 and must contain at least one uppercase letter, one lowercase letter and one number!", "OK");
                return false;
            }
            if (password != spassword)
            {
                _pageDialog.DisplayAlertAsync("", "Passwords must match!", "OK");
                return false;
            }
            return true;
        }
    }
}
