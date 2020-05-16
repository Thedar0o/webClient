using ClientWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientWebService.Data
{
    public class ClientsData : DbContext
    {
        //Constructor with connection string
        public ClientsData() : base("ClientsData")
        {

        }

        public DbSet<Contacts> Contacts { get; set; }
      //  public DbSet<Category> Categories { get; set; }
       // public DbSet<SubCategory> subCategories{ get; set; }
    }
}