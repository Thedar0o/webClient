using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
       ContactDBEntities db = new ContactDBEntities();
       

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

        public ActionResult Create()
        {
            ViewBag.categoryname = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

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

        public ActionResult Edit(int Id)
        {
            ViewBag.CategoryName = new SelectList(db.Categories, "Id", "CategoryName");
            var IdOfAccount = db.Contacts.Find(Id);
            return View(IdOfAccount);            
        }

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

        public ActionResult Delete(int Id)
        {
            using(var dbm = new ContactDBEntities())
            {
                return View(dbm.Contacts.Where(x => x.Id == Id).FirstOrDefault());
            }
        }

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

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int Id, FormCollection collection)
        //{
        //    try
        //    {
        //        using(var dbm = new ContactDBEntities())
        //        {
        //            Contacts contact = dbm.Contacts.Where(x => x.Id == Id).FirstOrDefault();
        //            dbm.Contacts.Remove(contact);
        //            db.SaveChanges();
        //            return RedirectToAction("Contacts");
        //        }
        //    }
        //    catch
        //    {
        //        return View("Contacts");
        //    }
        //}

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