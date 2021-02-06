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
    public class FeatureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Feature> features = db.Features.ToList();
            ViewBag.Features = features;
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                Feature feature = db.Features.Find(id);
                if (feature != null)
                {
                    return View(feature);
                }
                return HttpNotFound("Couldn't find the feature with id " + id.ToString() + "!");
            }


            return HttpNotFound("Missing feature id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Feature feature = new Feature();

            ViewBag.CamerasList = GetAllCameras();
            feature.Cameras = new List<Camera>();

            return View(feature);
        }



        [HttpPost]
        public ActionResult New(Feature featureRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Features.Add(featureRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Feature");
                }
                return View(featureRequest);
            }
            catch (Exception e)
            {
                return View(featureRequest);
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Feature feature = db.Features.Find(id);
                if (feature == null)
                {
                    return HttpNotFound("Couldn't find the feature with id " + id.ToString());
                }
                
                return View(feature);
            }
            return HttpNotFound("Missing feature id parameter!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Feature featureRequest)
        {

            try
            {
                
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("IF MODELSTATE IS VALID");
                    Feature feauture = db.Features.Include("Cameras")
                        .SingleOrDefault(b => b.FeatureID.Equals(id));
                    if (TryUpdateModel(feauture))
                    {
                        Debug.WriteLine("TryUpdateModel");
                        feauture.Name = featureRequest.Name;

                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Feature");
                }
                Debug.WriteLine("Not updated");
                return View(featureRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("Exception, not updated");
                return View(featureRequest);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Feature feature = db.Features.Find(id);
            if (feature != null)
            {
                db.Features.Remove(feature);
                db.SaveChanges();
                return RedirectToAction("Index", "Feature");
            }
            return HttpNotFound("Couldn't find the feature with id " + id.ToString() + "!");
        }


        public IEnumerable<SelectListItem> GetAllCameras()
        {
            // generam o lista goala
            var selectList = new List<SelectListItem>();
            foreach (var camera in db.Cameras.ToList())
            {
                // adaugam in lista elementele necesare pt dropdown
                selectList.Add(new SelectListItem
                {
                    Value = camera.CameraID.ToString(),
                    Text = camera.Resolution.ToString()
                }) ;
            }
            // returnam lista pentru dropdown
            return selectList;
        }
    }
}