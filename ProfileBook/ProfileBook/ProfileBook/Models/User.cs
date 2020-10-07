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
        private List<Contact> listOfContacts;

        [PrimaryKey, AutoIncrement, Column("id")]
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
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
