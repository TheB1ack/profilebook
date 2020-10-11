using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.Services.Profile
{
    public interface IProfileService
    {
        void AddEditContact(string newName, string newNick, string mewDescription, string newImage, string userLogin, Contact oldContact);
        void RemoveContact(Contact oldContact);
        List<Contact> GetListOfContacts(string userLogin);
    }
}
