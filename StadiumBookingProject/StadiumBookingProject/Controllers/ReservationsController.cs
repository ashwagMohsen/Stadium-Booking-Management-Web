using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StadiumBookingProject.Models;

namespace StadiumBookingProject.Controllers
{
    public class ReservationsController : Controller
    {
        private BookingEntities db = new BookingEntities();

        // GET: Reservations
        //[Authorize(Roles = "Admin")]
        //[Authorize]
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Accounts).Include(r => r.Stadiums).Include(r => r.Services);
            return View(reservations.ToList());
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            var reservations = db.Reservations.Include(r => r.Accounts).Include(r => r.Stadiums).Include(r => r.Services);
            return View(reservations.Where(a => a.full_name == name).ToList());
        }
        // GET: Reservations/Details/5
        //[Authorize(Roles = "Admin")]
        //[Authorize]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // GET: Reservations/Create
        //[Authorize(Users = "Client")]
        [AllowAnonymous]
        public ActionResult Create(int S_id)
        {
            var y = db.Stadiums.Where(a => a.Id == S_id).ToList();
            if (y.Count != 0)
            {
                ViewBag.hour = y[0].Hour;
                ViewBag.price1 = y[0].Price;
                ViewBag.Stadium_Id = y[0].Name;
            }
           
        //ViewBag.Acc_Id = new SelectList(db.Accounts, "Id", "Email");
            //ViewBag.Stadium_Id = new SelectList(db.Stadiums.Where(s => s.Id==S_id), "Id", "Name");
            ViewBag.Serv_Id = new SelectList(db.Services, "Serv_Id", "Serv_Name");
            return View();
        }
        [AllowAnonymous]
        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string date, string full_name, string earnest, string residual, int time, string mobile, string Stadium_Id, int Serv_Id, string Email,int hours, string price)
        {
            Reservations reservations2 = new Reservations();
            var E_Id = db.Accounts.Where(a => a.Email ==Email).ToList();
            var S_Id = db.Stadiums.Where(a => a.Name == Stadium_Id).ToList();
            if (ModelState.IsValid)
            {
                reservations2.Acc_Id = E_Id[0].Id; reservations2.date = date; reservations2.full_name = full_name; reservations2.earnest = earnest;
                reservations2.residual = residual; reservations2.time = time; reservations2.mobile = mobile; reservations2.Stadium_Id = S_Id[0].Id; reservations2.Serv_Id = Serv_Id;
                reservations2.price = price;reservations2.hours = hours;
                var list1 = db.Reservations.Where(a => a.Stadium_Id == reservations2.Stadium_Id && a.date == date && a.time == time).ToList();

                if (list1.Count != 0)
                {
                    ViewBag.message = "!!عذرا هذا التاريخ محجوز ,اختر تاريخ اخر";
                    var y2 = db.Stadiums.Where(a => a.Id == reservations2.Stadium_Id).ToList();
                    if (y2.Count != 0)
                    {
                        ViewBag.hour = y2[0].Hour;
                        ViewBag.price = y2[0].Price;
                        ViewBag.Stadium_Id = y2[0].Name;
                    }

                    ViewBag.Acc_Id = new SelectList(db.Accounts, "Id", "Email");
                    ViewBag.Serv_Id = new SelectList(db.Services, "Serv_Id", "Serv_Name");
                    return View();
                }
                else
                {
                    db.Reservations.Add(reservations2);
                    db.SaveChanges();
                    var x = db.Reservations.ToList();
                    int id = x[(x.Count) - 1].id;
                    return RedirectToAction("../StadiumBooking/Invoice", new { id1 = id });
                }
       
            }
            var y = db.Stadiums.Where(a => a.Id == S_Id[0].Id).ToList();
            if (y.Count != 0)
            {
                ViewBag.hour = y[0].Hour;
                ViewBag.price = y[0].Price;
                ViewBag.Stadium_Id = y[0].Name;

            }
            ViewBag.Acc_Id = new SelectList(db.Accounts, "Id", "Email");
            ViewBag.Serv_Id = new SelectList(db.Services, "Serv_Id", "Serv_Name");
            return View();
        }



        // GET: Reservations/Delete/5
        //[Authorize(Roles = "Admin")]
        //[Authorize]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }
        //[Authorize]

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            db.Reservations.Remove(reservations);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public JsonResult getLecture1(string S_name)
        //{

        //    IQueryable<Services> dbList = db.Services.Where(a => a.Serv_Name == S_name);

        //    return Json(dbList.ToList(), JsonRequestBehavior.AllowGet);
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
