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
            using (ContactDBEntities db = new ContactDBEntities())
            {
                if (db.Account.Any(x => x.Login == account.Login))
                {
                    ViewBag.DuplicateMessage = "Username already exist";
                    return View("AddUser");
                }
                account.Password = PasswordSecurity.PasswordStorage.CreateHash(account.Password);
                account.ConfirmPassword = account.Password;
                db.Account.Add(account);
                try
                {
                    db.SaveChanges();

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Reqistration Successful";
            return View("AddUser", new Account());
        }
    }
}