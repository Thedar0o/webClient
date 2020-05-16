using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlTypes;

namespace ClientWebService.Models
{
    public class Contacts
    {
        public int ContactsID { get; set; }
       // public int CategoryID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "SecoundName is required")]
        public string SecoundName { get; set; }        
        [Required(ErrorMessage = "Mail is required")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public SqlDateTime DateOfBirth { get; set; }

        //Navigation props to Category table 
      //  public virtual Category Category { get; set; }        

    }
}