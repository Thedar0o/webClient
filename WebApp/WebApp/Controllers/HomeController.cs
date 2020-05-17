using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        ContactDBEntities db = new ContactDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {            
            var contact = db.Contacts.ToList();
            return View(contact.ToList());
        }

        //public ActionResult Contacts()
        //{
        //    var contact = db.Contacts.ToList();
        //    return View(contact);
        //}

        //public JsonResult GetContacts()
        //{
        //    var cont = db.Contacts.ToArray();
        //    List<Contacts> listcont = new List<Contacts>();
        //    foreach (var item in cont)
        //    {
        //        listcont.Add(new Contacts
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            SecoundName = item.SecoundName,
        //            Email = item.Email,
        //            Password = item.Password,
        //            CategoriesId = item.CategoriesId,
        //            PhoneNumber = item.PhoneNumber,
        //            BirthDate = item.BirthDate,

        //        });
        //    }
        //    return Json(listcont, JsonRequestBehavior.AllowGet);
        //}
    }
}