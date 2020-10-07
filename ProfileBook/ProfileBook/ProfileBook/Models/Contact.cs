using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    [Table("Contacts")]
    class Contact
    {
        private int contactId;
        private int userId;
        private string nickName;
        private string fullName;
        private DateTime addTime;

        public int UserId
        {
            get { return userId; }
            private set { userId = value; }
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ContactId
        {
            get { return contactId; }
            private set { contactId = value; }
        }
        public string NickName
        {
            get { return nickName; }
            private set { nickName = value; }
        }
        public string FullName
        {
            get { return fullName; }
            private set { fullName = value; }
        }
        public DateTime AddTime
        {
            get { return addTime; }
            private set { addTime = value; }
        }


    }
}
