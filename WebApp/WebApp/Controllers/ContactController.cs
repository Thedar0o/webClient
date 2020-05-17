using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
       ContactDBEntities db = new ContactDBEntities();
       
        // GET: Contact
        public ActionResult Contacts()
        {
            var contact = db.Contacts.ToList();
            return View("Contacts", contact);
        }

        public ActionResult Details(int Id = 0)
        {
            var IdOfAccount = db.Contacts.Find(Id);
            return View(IdOfAccount);
        }

        public JsonResult GetContacts()
        {
            var cont = db.Contacts.ToArray();
            List<Contacts> listcont = new List<Contacts>();
            foreach (var item in cont)
            {
                listcont.Add(new Contacts
                {
                    Id = item.Id,
                    Name = item.Name,
                    SecoundName = item.SecoundName,
                    Email = item.Email,
                    Password = item.Password,
                    CategoriesId = item.CategoriesId,
                    PhoneNumber = item.PhoneNumber,
                    BirthDate = item.BirthDate,

                });
            }
            return Json(listcont, JsonRequestBehavior.AllowGet);
        }
    }
}