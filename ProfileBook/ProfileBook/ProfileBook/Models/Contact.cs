using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProfileBook.Models
{
    [Table("Contacts")]
    public class Contact
    {      
        [PrimaryKey, AutoIncrement]
        public int ContactId { get; set; }
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public string AddTime { get; set; }
    }
}
