using Shopping_Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopping_Site.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BatteryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Battery> batteries = db.Batteries.ToList();
            ViewBag.Batteries = batteries;
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Battery battery = db.Batteries.Find(id);
                if (battery != null)
                {
                    return View(battery);
                }
                return HttpNotFound("Couldn't find the battery with id " + id.ToString() + "!");
            }


            return HttpNotFound("Missing battery id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Battery battery = new Battery();

            ViewBag.PhonesList = GetAllPhones();
            battery.Phones = new List<Phone>();

            return View(battery);
        }



        [HttpPost]
        public ActionResult New(Battery batteryRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.PhonesList = GetAllPhones();
                    db.Batteries.Add(batteryRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Battery");
                }
                return View(batteryRequest);
            }
            catch (Exception e)
            {
                return View(batteryRequest);
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Battery battery = db.Batteries.Find(id);
                if (battery == null)
                {
                    return HttpNotFound("Couldn't find the battery with id " + id.ToString());
                }
                ViewBag.PhonesList = GetAllPhones();
                return View(battery);
            }
            return HttpNotFound("Missing battery id parameter!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Battery batteryRequest)
        {

            try
            {
                ViewBag.PhonesList = GetAllPhones();
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("IF MODELSTATE IS VALID");
                    Battery battery = db.Batteries.Include("Phones")
                        .SingleOrDefault(b => b.BatteryID.Equals(id));
                    if (TryUpdateModel(battery))
                    {
                        Debug.WriteLine("TryUpdateModel");
                        battery.Name = batteryRequest.Name;
                        battery.Capacity = batteryRequest.Capacity;
                        

                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Battery");
                }
                Debug.WriteLine("Not updated");
                return View(batteryRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("Exception, not updated");
                return View(batteryRequest);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Battery battery = db.Batteries.Find(id);
            if (battery != null)
            {
                db.Batteries.Remove(battery);
                db.SaveChanges();
                return RedirectToAction("Index", "Battery");
            }
            return HttpNotFound("Couldn't find the battery with id " + id.ToString() + "!");
        }


        public IEnumerable<SelectListItem> GetAllPhones()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            foreach (var phone in db.Phones.ToList())
            {
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = phone.PhoneID.ToString(),
                    Text = phone.Name
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
    }
}