using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using System.IO;
using Silkways.Models.SilkwaysFunction;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        private static GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult AddService(int? ID)
        {
            if (ID != null && ID > 0)
            {
                Service Edit = new ServiceModel().GetServiceByID(Convert.ToInt32(ID));
                return View(Edit);
            }
            else
            {
                Service S = new Service();
                return View(S);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddService(Service Service, HttpPostedFileBase file, string oldimage)
        {
            if (file != null)
            {
                Service.Image = GernalFunction.SaveFile(file, Constants.PathServiceImages);
            }
            else if (!string.IsNullOrEmpty(oldimage))
            {
                Service.Image = oldimage;
            }
            else
            {
                Service.Image = Constants.NoImage;
            }

            if (Service != null && Service.ID > 0)
            {
                //Service.Url = GernalFunction.GenerateUrl(Service.Title);
                new ServiceModel().Update(Service);
                TempData["AlertTask"] = "Record Updated Successfully";
            }
            else
            {
                //Service.Url = GernalFunction.GenerateUrl(Service.Title);
                //Service S = db.Services.FirstOrDefault(a => a.Url == Service.Url);
                //if (S != null && S.ID > 0)
                //{
                //    Service.Url = Service.Url + "-1";
                //}

                new ServiceModel().Save(Service);
                TempData["AlertTask"] = "Record Saved Successfully";
            }
            return RedirectToAction("ManageService");
        }

        public ActionResult ManageService()
        {
            List<Service> lst = new ServiceModel().GetAllServices();
            return View(lst);
        }
        public ActionResult Delete(int ID)
        {
            bool IsDelete = new ServiceModel().Delete(ID);
            if (IsDelete)
            {
                TempData["AlertTask"] = "Record Deleted Successfully";
            }
            else
            {
                TempData["AlertTask"] = "An Error Occured";
            }
            return RedirectToAction("ManageService");
        }
        public static List<Category> GetCategories()
        {
            return new ServiceModel().GetListOfCategories();
        }
        public static List<Service> getallServices()
        {
            return db.Services.ToList();
        }
    }
}