using CattlewatchMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CattlewatchMVC.Controllers
{
    public class CDashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CDashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLocation()
        {
            return View();
        }
        public ActionResult Req()
        {
            return View();
        }
        public ActionResult Requesting()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Requesting(Order order)
        {
            var userId = User.Identity.GetUserId();
            var userName = User.Identity.GetUserName();
            var livestock = order.SelectedLivestock.ToList(); /*string.Join(",", order.SelectedLivestock);*/

            order.CLinetId = userId;
            order.ClinetName = userName;
            order.OrderStatus = "Open";
            db.Orders.Add(order);

            foreach (var items in livestock.ToList())
            {
                if (ModelState.IsValid)
                {
                    LivestockOrder ls = new LivestockOrder();

                    ls.LivestockId = Convert.ToInt32(livestock.FirstOrDefault());
                    ls.OrderId = order.OrderId;                   
                    db.LivestockOrders.Add(ls);
                    db.SaveChanges();
                    var itemRemove = livestock.FirstOrDefault();
                    livestock.Remove(itemRemove);



                }

            }
            
            return RedirectToAction("Numbers");
        }
        public ActionResult Numbers()
        {
            string userId = User.Identity.GetUserId();
            var orderId = db.Orders.Where(x => x.CLinetId == userId).Select(x => x.OrderId).FirstOrDefault();//livestockorders will have to be removed when order is closed
            var livestockOrder = db.LivestockOrders.Include(x => x.Livestock).Where(x => x.OrderId.ToString() == orderId.ToString()).ToList();
            return View(livestockOrder);
        }

        public ActionResult AddEditLivestock(int? LivestockOrderId)
        {
            if (LivestockOrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockOrder livestockOrder = db.LivestockOrders.Find(LivestockOrderId);
            if (livestockOrder == null)
            {
                return HttpNotFound();
            }

            return PartialView("Partial", livestockOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Numbers([Bind(Include = "LivestockOrderId,number,LivestockId,OrderId,LocationId")]LivestockOrder ls)
        {

            db.Entry(ls).State = EntityState.Modified;
            db.SaveChanges();

            return View(ls);
        }
        public ActionResult GetIfSameLocation()
        {

            string userId = User.Identity.GetUserId();
            var order = db.Orders.Where(x => x.CLinetId == userId).FirstOrDefault();
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetLocation([Bind(Include = "OrderId,ClinetName,CLinetId,SameLocation,location,")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            if (order.SameLocation == "True")
            {
                return RedirectToAction("SameLocation");
            }
            else
            {
                return RedirectToAction("NotSameLocation");
            }
            //var userId = User.Identity.GetUserId();
            //var orderId = db.Orders.Where(x => x.CLinetId == userId).Select(x => x.OrderId).FirstOrDefault();

            //var livestockOrder = db.LivestockOrders.Where(x => x.OrderId == orderId).ToList();
            //int totalAnimals = 0;
            //foreach (var item in livestockOrder)
            //{
            //    item.number += totalAnimals;
            //}

            //Order o = db.Orders.Find(orderId);
            //o.TotalAnimals = totalAnimals;
         
        }
        public ActionResult SameLocation()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SameLocation(string Latitude, string Longitude, string locality, Location location)
        {
            if (ModelState.IsValid)
            {
                location.longitude = Longitude;
                location.Latitude = Latitude;
                location.CityName = locality;

                db.Locations.Add(location);
                string userId = User.Identity.GetUserId();
                var orderId = db.Orders.Where(x => x.CLinetId == userId).Select(x => x.OrderId).FirstOrDefault();
                var livestockOrderlist = db.LivestockOrders.Where(x => x.OrderId == orderId).ToList();
                foreach (var item in livestockOrderlist)
                {
                    item.LocationId = location.LocationId;
                    db.LivestockOrders.Add(item);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult NotSameLocation()
        {
            string userId = User.Identity.GetUserId();
            var orderId = db.Orders.Where(x => x.CLinetId == userId).Select(x => x.OrderId).FirstOrDefault();//livestockorders will have to be removed when order is closed
            var livestockOrder = db.LivestockOrders.Include(x => x.Livestock).Where(x => x.OrderId.ToString() == orderId.ToString()).ToList();
            return View(livestockOrder);
        }
        public ActionResult AddEditLocation(int? LivestockOrderId)
        {
            if (LivestockOrderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LivestockOrder livestockOrder = db.LivestockOrders.Find(LivestockOrderId);
            if (livestockOrder == null)
            {
                return HttpNotFound();
            }

            return PartialView("Partial2", livestockOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddsingleLocation(string Latitude, string Longitude, string locality, [Bind(Include = "LivestockOrderId,number,LivestockId,OrderId,LocationId")]LivestockOrder ls)
        {
            Location location = new Location();
            location.longitude = Longitude;
            location.Latitude = Latitude;
            location.CityName = locality;
            db.Locations.Add(location);
            ls.LocationId = location.LocationId;
            db.Entry(ls).State = EntityState.Modified;
            db.SaveChanges();

            return View(ls);
            
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetLocation([Bind(Include = "LocationId,CityName,Latitude,longitude,Description")] Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Locations.Add(location);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(location);
        //}
        public JsonResult GetAllLocation()
        {
            var data = db.Locations.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllLocationById(int id)
        {
            var data = db.Locations.Where(x => x.LocationId == id).SingleOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClientDashboard()
        {
            ViewBag.ListOfDropdown = db.Locations.ToList();
            ViewBag.lat1 = "-29.792179";
            ViewBag.lng1 = "30.823179";


            return View();
        }
        
    }
}