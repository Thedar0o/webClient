using ClientWebService.Data;
using ClientWebService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientWebService.Controllers
{
    public class HomeController : Controller
    {
        private ClientsData dataBase = new ClientsData();
        private static readonly string Date = "yyy-MM-dd HH:mm:ss";
        public string kurwa;

        public ActionResult Index()
        {
            using (var db = dataBase)
            {
                //Category category = new Category { CategoryID = 1, CategoryName = "Boss" };
                //db.Categories.Add(category);
                try
                {
                    string pareseDate = "01-01-1991 12:13:14";                    
                    DateTime dtt = Convert.ToDateTime(pareseDate, CultureInfo.InvariantCulture);
                    //var dt = DateTime.ParseExact(pareseDate, Date, CultureInfo.InvariantCulture);
                    Contacts contacts = new Contacts { ContactsID = 1, Name = "John", SecoundName = "Kowalskey", Mail = "kowalskey.j@mail.com", Password = "Abc123!", PhoneNumber = 123456789, DateOfBirth = new DateTime(1991,1,1) };
                    db.Contacts.Add(contacts);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
            }


            return View();

        }
    }
}