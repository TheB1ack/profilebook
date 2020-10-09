using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    [Table("Contacts")]
    class Contact
    {      
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ContactId { get; set; }
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string FullName { get; set; }
        public DateTime AddTime { get; set; }
    }
}
