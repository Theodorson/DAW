using Shopping_Site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Shopping_Site.Controllers
{

    public class PhoneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Home()
        {
            return View();
        }


        public ActionResult Index()
        {
            List<Phone> phones = db.Phones.ToList();
            ViewBag.Phones = phones;
            return View();
        }

        public ActionResult Details(int? id)
        {   

            if (id.HasValue)
            {
                Phone phone = db.Phones.Find(id);
                if (phone != null)
                {
                    return View(phone);
                }
                return HttpNotFound("Couldn't find the phone with id " + id.ToString() + "!");
            }


            return HttpNotFound("Missing phone id parameter!");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            Phone phone = new Phone();

            // toate procesoarele, camerele si bateriile telefoanelor
            ViewBag.ProcessorsList = GetAllProcessors();
            ViewBag.BatteriesList = GetAllBatteries();

            phone.Cameras = new List<Camera>();
            phone.Battery = new Battery();
            phone.Processor = new Processor();
            phone.CamerasList = GetAllCameras();

            return View(phone);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult New(Phone phoneRequest)
        {
            var selectedCameras = phoneRequest.CamerasList.Where(b => b.Checked).ToList();
            ViewBag.ProcessorsList = GetAllProcessors();
            ViewBag.BatteriesList = GetAllBatteries();
            try
            {
                if (ModelState.IsValid)
                {
                    phoneRequest.Cameras = new List<Camera>();
                    for (int i = 0; i < selectedCameras.Count(); i++)
                    {
                        // ii adaugam camerei, caracteristicile selectate
                        Camera camera = db.Cameras.Find(selectedCameras[i].Id);
                        phoneRequest.Cameras.Add(camera);
                    }
                    phoneRequest.Stock = true;
                    db.Phones.Add(phoneRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Phone");
                }
                return View(phoneRequest);
            }
            catch (Exception e)
            {
                return View(phoneRequest);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Phone phone = db.Phones.Find(id);
                phone.CamerasList = GetAllCameras();
                foreach (Camera checkedCamera in phone.Cameras)
                {   // iteram prin genurile care erau atribuite cartii inainte de momentul accesarii formularului
                    // si le selectam/bifam  in lista de checkbox-uri
                    phone.CamerasList.FirstOrDefault(g => g.Id == checkedCamera.CameraID).Checked = true;
                }
                if (phone == null)
                {
                    return HttpNotFound("Couldn't find the phone with id " + id.ToString());
                }
                ViewBag.ProcessorsList = GetAllProcessors();
                ViewBag.CamerasList = GetAllCameras();
                ViewBag.BatteriesList = GetAllBatteries();
                return View(phone);
            }
            return HttpNotFound("Missing phone id parameter!");
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Phone phoneRequest)
        {
            var selectedCameras = phoneRequest.CamerasList.Where(b => b.Checked).ToList();
            Phone phone = db.Phones.Include("Processor").Include("Battery")
                .SingleOrDefault(b => b.PhoneID.Equals(id));
            ViewBag.ProcessorsList = GetAllProcessors();
            ViewBag.BatteriesList = GetAllBatteries();
            try
            {
                
                if (ModelState.IsValid)
                {
                   
                    if (TryUpdateModel(phone))
                    {
                        phone.Name = phoneRequest.Name;
                        phone.Brand = phoneRequest.Brand;
                        phone.Capacity = phoneRequest.Capacity;
                        phone.Memory = phoneRequest.Memory;
                        phone.Color = phoneRequest.Color;
                        phone.Operating_system = phoneRequest.Operating_system;
                        phone.Image = phoneRequest.Image;
                        phone.Price = phoneRequest.Price;
                        phone.SIM_slots = phoneRequest.SIM_slots;
                        phone.Type = phoneRequest.Type;
                        phone.ProcessorID = phoneRequest.ProcessorID;
                        phone.BatteryID = phoneRequest.BatteryID;
                        phone.Stock = phoneRequest.Stock;
                        phone.Cameras.Clear();
                        phone.Cameras = new List<Camera>();

                        for (int i = 0; i < selectedCameras.Count(); i++)
                        {
                            // ii adaugam camerei, caracteristicile selectate
                            Camera camera = db.Cameras.Find(selectedCameras[i].Id);
                            phone.Cameras.Add(camera);
                        }


                        
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Phone");
                }
               
                return View(phoneRequest);
            }
            catch (Exception e)
            { 
                return View(phoneRequest);
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Phone phone = db.Phones.Find(id);
            if (phone != null)
            {
                db.Phones.Remove(phone);
                db.SaveChanges();
                return RedirectToAction("Index", "Phone");
            }
            return HttpNotFound("Couldn't find the phone with id " + id.ToString() + "!");
        }


        [NonAction] // specificam faptul ca nu este o actiune
        public IEnumerable<SelectListItem> GetAllProcessors()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            foreach (var processor in db.Processors.ToList())
            {
                var processor_info = processor.Model + " " + processor.Frequency + " GHz";
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = processor.ProcessorID.ToString(),
                    Text = processor_info
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }

        [NonAction] // specificam faptul ca nu este o actiune
        public List<CheckBoxViewModel> GetAllCameras()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var camera in db.Cameras.ToList())
            {
                var camera_info = camera.Resolution.ToString() + " MP " + camera.Type;
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = camera.CameraID,
                    Name = camera_info,
                    Checked = false
                });
            }
            return checkboxList;
        }


        [NonAction] // specificam faptul ca nu este o actiune
        public IEnumerable<SelectListItem> GetAllBatteries()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            foreach (var battery in db.Batteries.ToList())
            {
                var battery_info = battery.Name + " " + battery.Capacity.ToString() + "mAh";
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = battery.BatteryID.ToString(),
                    Text = battery_info
                });
            }
            // returnam lista pentru dropdown
            return selectList;
        }
    }
}