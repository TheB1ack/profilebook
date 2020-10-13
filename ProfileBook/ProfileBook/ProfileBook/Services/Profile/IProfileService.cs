using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfileBook.Services.Profile
{
    public interface IProfileService
    {
        void AddOrEditContact(string newName, string newNick, string mewDescription, string newImage, int userId, Contact oldContact);
        Task<List<Contact>> RemoveContactAsync(Contact oldContact, int userId);
        Task<List<Contact>> GetListOfContacts(int userId);
    }
}
