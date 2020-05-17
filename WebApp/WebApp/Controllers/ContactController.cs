using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Class to manage contacts, it has crud methods
    /// </summary>
    public class ContactController : Controller
    {
        /// <summary>
        /// Create new db entities
        /// </summary>
       ContactDBEntities db = new ContactDBEntities();       

        /// <summary>
        /// Return list of contacts to the View
        /// </summary>
        /// <returns></returns>
        public ActionResult Contacts()
        {
            var contact = db.Contacts.ToList();
            return View("Contacts", contact);
        }

        /// <summary>
        /// Return new view with details of specific contact
        /// </summary>
        /// <param name="Id">contact id</param>
        /// <returns></returns>
        public ActionResult Details(int Id = 0)
        {
            var IdOfAccount = db.Contacts.Find(Id);
            return View(IdOfAccount);
        }
        /// <summary>
        /// Get method for creating an contacts
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ///Get dictionary with dynamic view data
            ViewBag.categoryname = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        /// <summary>
        /// POST method to creating contacts from view parameters and save it to DB
        /// </summary>
        /// <param name="contacts">Contacts data model, </param>
        /// <param name="categories">Categories data model</param>
        /// <returns>return RedirectToAction</returns>
        [HttpPost]
        public ActionResult Create(Contacts contacts, Categories categories)
        {
            try
            {
                using(var dbm = new ContactDBEntities())
                {
                    contacts.CategoriesId = 3;
                    var cat = dbm.Categories;
                    var id = cat.Where(x => x.Id == categories.Id).FirstOrDefault();
                    try
                    {
                        contacts.CategoriesId = id.Id;
                    }
                    catch 
                    {
                        //while category is not chosen, default value is "other"
                        contacts.CategoriesId = 3;
                    }                   
                    dbm.Contacts.Add(contacts);
                    dbm.SaveChanges();
                }
                return RedirectToAction("Contacts");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Get method for edditing contats, ti finding an contats with specific ID
        /// </summary>
        /// <param name="Id">contats id to edit</param>
        /// <returns>Return specific contats to the view</returns>
        public ActionResult Edit(int Id)
        {
            ///Get dictionary with dynamic view data
            ViewBag.CategoryName = new SelectList(db.Categories, "Id", "CategoryName");
            var acc = db.Contacts.Find(Id);
            return View(acc);            
        }

        /// <summary>
        /// POST method to edit contacts, it gets contats model values and save changes to DB
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="category"></param>
        /// <returns>return RedirectToAction contacts</returns>
        [HttpPost]
        public ActionResult Edit(Contacts contacts, Categories category)
        {
            try
            {
                using(var dbm = new ContactDBEntities())
                {
                    try
                    {
                        contacts.CategoriesId = int.Parse(Request.Form["CategoryName"]);
                    }
                    catch (Exception)
                    {
                        //while category is not chosen, default value is "other"
                        contacts.CategoriesId = 3;
                    }                  
                    dbm.Entry(contacts).State = EntityState.Modified;
                    dbm.SaveChanges();
                }
                return RedirectToAction("Contacts");
            }
            catch
            {
                return View("Contacts");
            }
        }

        /// <summary>
        /// Get method for deleting contacts
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Delete(int Id)
        {
            using(var dbm = new ContactDBEntities())
            {
                return View(dbm.Contacts.Where(x => x.Id == Id).FirstOrDefault());
            }
        }

        /// <summary>
        /// POST method for deleting contacts from DB, it gets specific contact and get acces to DB to overwrite it
        /// </summary>
        /// <param name="contacts">Contacts model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Contacts contacts)
        {
            try
            {
                using (var dbm = new ContactDBEntities())
                {
                    Contacts contact = dbm.Contacts.Where(x => x.Id == contacts.Id).FirstOrDefault();
                    var del = db.Contacts.Find(contacts.Id);
                    db.Entry(del).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("Contacts");
                }
            }
            catch 
            {
                return View("Contacts");
            }
        }

        /// <summary>
        /// JsonResult method to save specific contacts in json file
        /// </summary>
        /// <returns>Return Json with contacts parameters</returns>
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