using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    [Table("Users")]
    class User
    {
        
        private int userId;
        private string userLogin;
        private string userPassword;
        private List<Contact> listOfContacts;

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
            }
        }
        public string UserLogin
        {
            get { return userLogin; }
            set
            {
                userLogin = value;
            }
        }
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
            }
        }

        [Ignore]
        public List<Contact> ListOfContacts
        {
            get { return listOfContacts; }
            set
            {
                listOfContacts = value;
            }
        } 

    }
}
