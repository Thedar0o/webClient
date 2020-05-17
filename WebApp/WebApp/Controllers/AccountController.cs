using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            int userID = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VerifyAccount(Account account)
        {
            using (ContactDBEntities db = new ContactDBEntities())
            {
                if (account.Login != null && account.Password != null)
                {
                    var userData = db.Account.FirstOrDefault(x => x.Login == account.Login);
                    if(userData == null)
                    {
                        account.LoginErrorMessage = "Wrong username/password";
                        return View("Login", account);
                    }
                    bool res = PasswordSecurity.PasswordStorage.VerifyPassword(account.Password, userData.Password);
                    if (res)
                    {
                        Session["Id"] = userData.Id;
                        Session["userName"] = userData.Login;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        account.LoginErrorMessage = "Wrong username/password";
                        return View("Login", account);
                    }
                }
                else
                {
                    account.LoginErrorMessage = "Wrong username/password";
                    return View("Login", account);
                }
            }
            //using(ContactDBEntities db = new ContactDBEntities())
            //{
                //var userData = db.Account.FirstOrDefault(x => x.Login == account.Login && x.Password == account.Password);
                //if(userData == null)
                //{
                //    account.LoginErrorMessage = "Wrong username/password";
                //    return View("Login", account);
                //}
                //else
                //{
                //    Session["Id"] = userData.Id;
                //    Session["userName"] = userData.Login;
                //    return RedirectToAction("Index", "Home");
                //}            
            //return View();
        }

        [HttpGet]
        public ActionResult AddUser(int Id = 0)
        {
            Account account = new Account();
            return View(account);
        }

        [HttpPost]
        public ActionResult AddUser(Account account)
        {
            using(ContactDBEntities db = new ContactDBEntities())
            {
                if(db.Account.Any(x => x.Login == account.Login))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    return View("AddUser");
                }
                account.Password = PasswordSecurity.PasswordStorage.CreateHash(account.Password);
                db.Account.Add(account);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Reqistration Successful";
            return View("AddUser", new Account());
        }
    }
}