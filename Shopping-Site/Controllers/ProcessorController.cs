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
    public class ProcessorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Processor> processors = db.Processors.ToList();
            ViewBag.Processors = processors;
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Processor processor = db.Processors.Find(id);
                if (processor != null)
                {
                    return View(processor);
                }
                return HttpNotFound("Couldn't find the processor with id " + id.ToString() + "!");
            }


            return HttpNotFound("Missing processor id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Processor processor = new Processor();

            ViewBag.PhonesList = GetAllPhones();
            processor.Phones = new List<Phone>();

            return View(processor);
        }



        [HttpPost]
        public ActionResult New(Processor processorRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.PhonesList = GetAllPhones();
                    db.Processors.Add(processorRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Processor");
                }
                return View(processorRequest);
            }
            catch (Exception e)
            {
                return View(processorRequest);
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Processor processor = db.Processors.Find(id);
                if (processor == null)
                {
                    return HttpNotFound("Couldn't find the processor with id " + id.ToString());
                }
                ViewBag.PhonesList = GetAllPhones();
                return View(processor);
            }
            return HttpNotFound("Missing processor id parameter!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Processor processorRequest)
        {

            try
            {
                ViewBag.PhonesList = GetAllPhones();
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("IF MODELSTATE IS VALID");
                    Processor processor = db.Processors.Include("Phones")
                        .SingleOrDefault(b => b.ProcessorID.Equals(id));
                    if (TryUpdateModel(processor))
                    {
                        Debug.WriteLine("TryUpdateModel");
                        processor.Model = processorRequest.Model;
                        processor.Cores_number = processorRequest.Cores_number;
                        processor.Frequency = processorRequest.Frequency;

                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Processor");
                }
                Debug.WriteLine("Not updated");
                return View(processorRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("Exception, not updated");
                return View(processorRequest);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Processor processor = db.Processors.Find(id);
            if (processor != null)
            {
                db.Processors.Remove(processor);
                db.SaveChanges();
                return RedirectToAction("Index", "Processor");
            }
            return HttpNotFound("Couldn't find the processor with id " + id.ToString() + "!");
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