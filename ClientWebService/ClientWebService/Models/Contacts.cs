using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWebService.Models
{
    public class Contacts
    {
        public int ContactsID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string SecoundName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Navigation props to Category table 
        public virtual Category Category { get; set; }
    }
}