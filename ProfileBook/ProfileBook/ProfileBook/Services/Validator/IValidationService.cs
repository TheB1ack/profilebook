using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Validator
{
    public interface IValidationService
    {
        bool VerifyInput(string login, string password, string spassword);
    }
}
