using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Contact> _repositoryC;
        private readonly IRepository<User> _repositoryU;
        public ProfileService(IRepository<Contact> repositoryС, IRepository<User> repositoryU)
        {
            _repositoryC = repositoryС;
            _repositoryU = repositoryU;
        }
        public void AddEditContact(string newName, string newNick, string newDescription, string newImage, string userLogin, Contact oldContact)
        {
            if (oldContact != null)
            {
                oldContact.FullName = newName;
                oldContact.NickName = newNick;
                oldContact.Description = newDescription;
                oldContact.ImageSource = newImage.ToString();
                _repositoryC.UpdateItemAsync(oldContact);
            }
            else
            {
                int userId = _repositoryU.GetUserByLogin(userLogin).UserId;
                Contact contact = new Contact()
                {
                    FullName = newName,
                    NickName = newNick,
                    ImageSource = newImage,
                    Description = newDescription,
                    AddTime = DateTime.Now.ToString(),
                    UserId = userId
                };
                _repositoryC.SaveItemAsync(contact);
            }
        }
        public void RemoveContact(Contact oldContact)
        {
            _repositoryC.DeleteItemAsync(oldContact);
        }
        public List<Contact> GetListOfContacts(string userLogin)
        {
            int userId = _repositoryU.GetUserByLogin(userLogin).UserId;
            return _repositoryC.GetContactsById(userId);
        }
    }
}
