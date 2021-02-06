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
    public class CameraController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Camera> cameras = db.Cameras.ToList();
            ViewBag.Cameras = cameras;
            return View();
        }


        public ActionResult Details(int? id)
        {   
            if (id.HasValue)
            {
                Camera camera = db.Cameras.Find(id);
                if (camera != null)
                {
                    return View(camera);
                }
                return HttpNotFound("Couldn't find the camera with id " + id.ToString() + "!");
            }


            return HttpNotFound("Missing camera id parameter!");
        }

        [HttpGet]
        public ActionResult New()
        {
            Camera camera = new Camera();
            camera.Phones = new List<Phone>();
            camera.Features = new List<Feature>();
            camera.FeaturesList = GetAllFeatures();
            return View(camera);
        }



        [HttpPost]
        public ActionResult New(Camera cameraRequest)
        {   
            var selectedFeatures = cameraRequest.FeaturesList.Where(b => b.Checked).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    cameraRequest.Features = new List<Feature>();
                    for (int i = 0; i < selectedFeatures.Count(); i++)
                    {
                        // ii adaugam camerei, caracteristicile selectate
                        Feature feature = db.Features.Find(selectedFeatures[i].Id);
                        cameraRequest.Features.Add(feature);
                    }
                    
                    db.Cameras.Add(cameraRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Camera");
                }
                return View(cameraRequest);
            }
            catch (Exception e)
            {
                return View(cameraRequest);
            }
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Camera camera = db.Cameras.Find(id);
                camera.FeaturesList = GetAllFeatures();

                foreach (Feature checkedFeature in camera.Features)
                {   // iteram prin genurile care erau atribuite cartii inainte de momentul accesarii formularului
                    // si le selectam/bifam  in lista de checkbox-uri
                   camera.FeaturesList.FirstOrDefault(g => g.Id == checkedFeature.FeatureID).Checked = true;
                }
                if (camera == null)
                {
                    return HttpNotFound("Couldn't find the camera with id " + id.ToString());
                }
                ViewBag.PhonesList = GetAllPhones();
                ViewBag.FeaturesList = GetAllFeatures();
                return View(camera);
            }
            return HttpNotFound("Missing camera id parameter!");
        }


        [HttpPut]
        public ActionResult Edit(int id, Camera cameraRequest)
        {
            var selectedFeatures = cameraRequest.FeaturesList.Where(b => b.Checked).ToList();
            try
            {
                ViewBag.PhonesList = GetAllPhones();
                ViewBag.FeaturesList = GetAllFeatures();
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("IF MODELSTATE IS VALID");
                    Camera camera = db.Cameras.Include("Phones").Include("Features")
                        .SingleOrDefault(b => b.CameraID.Equals(id));
                    if (TryUpdateModel(camera))
                    {
                        Debug.WriteLine("TryUpdateModel");
                        camera.Resolution = cameraRequest.Resolution;
                        camera.Flash = cameraRequest.Flash;
                        camera.Type = cameraRequest.Type;
                        camera.Features.Clear();
                        camera.Features = new List<Feature>();

                        for (int i = 0; i < selectedFeatures.Count(); i++)
                        {
                            // ii adaugam camerei, caracteristicile selectate
                            Feature feature = db.Features.Find(selectedFeatures[i].Id);
                            camera.Features.Add(feature);
                        }
                      
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Camera");
                }
                Debug.WriteLine("Not updated");
                return View(cameraRequest);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine("Exception, not updated");
                return View(cameraRequest);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Camera camera = db.Cameras.Find(id);
            if (camera != null)
            {
                db.Cameras.Remove(camera);
                db.SaveChanges();
                return RedirectToAction("Index", "Camera");
            }
            return HttpNotFound("Couldn't find the camera with id " + id.ToString() + "!");
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

        [NonAction]
        public List<CheckBoxViewModel> GetAllFeatures()
        {
            var checkboxList = new List<CheckBoxViewModel>();
            foreach (var feature in db.Features.ToList())
            {
                checkboxList.Add(new CheckBoxViewModel
                {
                    Id = feature.FeatureID,
                    Name = feature.Name,
                    Checked = false
                });
            }
            return checkboxList;
        }
    }
}