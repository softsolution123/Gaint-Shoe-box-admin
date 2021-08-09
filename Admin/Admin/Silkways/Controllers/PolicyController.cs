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
    public class PolicyController : Controller
    {
        // GET: Service
        private static GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult Add(int? ID)
        {
            if (ID != null && ID > 0)
            {
                Policy Edit = new PolicyModel().GetPolicyByID(Convert.ToInt32(ID));
                ViewBag.img += "<li style='width:215px; height:215px;'><img src='https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/PDF_file_icon.svg/625px-PDF_file_icon.svg.png'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                // ViewBag.img += "<li><img src='/Resources/Brand/" + Edit.BrandImage + "' value='" + Edit.ID + "'  width='76px'  height='54px' style='float: left; height:54px; width=76px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                return View(Edit);
            }
            else
            {
                Policy S = new Policy();
                return View(S);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Policy policy, HttpPostedFileBase file , string oldPDF)
        {
            if (file != null)
            {
                policy.FileName = GernalFunction.SaveFile(file, Constants.PathPolicyFiles);
            }
            else if (!string.IsNullOrEmpty(oldPDF))
            {
                policy.FileName = oldPDF;
            }

            else
            {
                policy.FileName = Constants.NoImage;
            }


            if (policy != null && policy.ID > 0)
            {

                new PolicyModel().Update(policy);
                TempData["AlertTask"] = "Record Updated Successfully";
            }
            else
            {


                new PolicyModel().Save(policy);
                TempData["AlertTask"] = "Record Saved Successfully";
            }
            return RedirectToAction("Manage");
        }

        public ActionResult Manage()
        {
            List<Policy> lst = new PolicyModel().GetAllPolicies();
            return View(lst);
        }
        public ActionResult Delete(int ID)
        {
            bool IsDelete = new PolicyModel().Delete(ID);
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