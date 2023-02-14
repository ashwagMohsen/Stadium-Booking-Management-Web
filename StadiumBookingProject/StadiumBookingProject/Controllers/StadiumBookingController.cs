using PdfSharp.Drawing;
using PdfSharp.Pdf;
using StadiumBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StadiumBookingProject.Controllers
{
    [AllowAnonymous]
    public class StadiumBookingController : Controller
    {
        static bool connect=false ;
        BookingEntities db = new BookingEntities();
        // GET: StadiumBooking

        public ActionResult Index()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult Index(int id1)
        {
            if (connect == true)
            {
                return RedirectToAction("Create", "Reservations", new { S_id= id1 });

            }
            else if (connect == false)
            {
                return RedirectToAction("Login");

            }
            return View();
        }
        //[Authorize(Users =TempData["woner"])]
        //[Authorize(Users = "Client")]
        public ActionResult Invoice(int id1)
        {
            var list1 = db.Reservations.Where(a => a.id == id1);
            return View(list1.ToList());
        }
      

        //------------------------------Register--------------------------

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string Email,string Password)
        {
            Accounts accounts1 = new Accounts();
            // await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
            if (ModelState.IsValid)
            {
                accounts1.Email = Email; accounts1.Password = Password; accounts1.WonerType = "عميل";
                db.Accounts.Add(accounts1);
                db.SaveChanges();
                //userManager.AddToUser("Client");
                //TempData["woner"] = "Client";
                connect = true;
                return RedirectToAction("Index");
            }
            return View(accounts1);
        }
        //------------------------------LogIn--------------------------
        public ActionResult Login()
        {
            //ViewBag.WonerType = new SelectList(db.Stadiums, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Email, string Password, string WonerType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var list1 = db.Accounts.ToList();

                    foreach (var i in list1)
                    {
                        if (i.Email == Email && i.Password == Password && i.WonerType== WonerType)
                        {
                            if (WonerType == "مدير")
                            {
                                //i.CreateIdentityAsync()
                                //TempData["woner"] = "Admin";
                                connect = true;
                                return RedirectToAction("Index","Reservations");
                            }
                            else
                            {
                                connect = true;
                                //TempData["woner"] = "Client";
                                return RedirectToAction("Index");
                            }

                        }
                        else
                        {
                            ViewBag.message="You must sign up at first !!!";
                        }
                    }
                }
                catch
                {

                }
              
                
            }
            return View();
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            connect = false;
            return RedirectToAction("Index", "StadiumBooking");
        }
    }
}