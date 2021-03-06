﻿using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileBook.Enums;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Contact> _repositoryC;
        public ProfileService(IRepository<Contact> repositoryС)
        {
            _repositoryC = repositoryС;
        }
        public void AddOrEditContact(string newName, string newNick, string newDescription, string newImage, int  userId, Contact oldContact)
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
        public async Task<List<Contact>> RemoveContactAsync(Contact oldContact, int userId, SortEnum parameter)
        {
            await _repositoryC.DeleteItemAsync(oldContact);
            return await GetListOfContacts(userId, parameter);
        }
        public async Task<List<Contact>> GetListOfContacts(int userId, SortEnum parameter)
        {
            var items = await _repositoryC.GetItemsAsync<Contact>();
            var list =  items.Where(x => x.UserId == userId).ToList();
            return SortListWithParameter(list, parameter);
        }
        public List<Contact> SortListWithParameter(List<Contact> oldList, SortEnum parameter)
        {
            List<Contact> list = null;
            if(oldList != null || oldList.Count() > 1)
            {
                switch ((int)parameter)
                {
                    case 0:
                        {
                            list = oldList.OrderBy(x => x.FullName).ToList();
                            break;
                        }
                    case 1:
                        {
                            list = oldList.OrderBy(x => x.NickName).ToList();
                            break;
                        }
                    case 2:
                        {
                            list = oldList.OrderBy(x => x.AddTime).ToList();
                            break;
                        }
                }
            }
            return list;
        }
    }
}
