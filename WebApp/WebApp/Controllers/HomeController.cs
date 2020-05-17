using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Simple class to control navigation in home page
    /// </summary>
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

    }
}