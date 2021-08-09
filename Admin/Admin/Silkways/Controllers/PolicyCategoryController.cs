using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class PolicyCategoryController : Controller
    {
        public ActionResult Add()
        {
            //var lst = new CategoryModel().GetAll().OrderBy(x => x.ID);
            ////var lst = new GernalFunction().CategoryList();
            //ViewBag.DDLCate = lst;
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                PolicyCategory cate = new PolicyCategoryModel().GetbyID(ID);
             
                return View(cate);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Add(PolicyCategory cate)
        {
          
           
           
          
           

            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                cate.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new PolicyCategoryModel().Update(cate);
                TempData["AlertTask"] = "Record Updated Successfully";
                return RedirectToAction("Manage");
            }
            else
            {
                new PolicyCategoryModel().Save(cate);
                TempData["AlertTask"] = "Record Saved Successfully";
                return RedirectToAction("Manage");
            }
            
    }

        public ActionResult Manage()
        {
            List<PolicyCategory> cate = new PolicyCategoryModel().GetAll();
            return View(cate);
        }

        public ActionResult Delete(int ID)
        {
          
      
            {
                new PolicyCategoryModel().Delete(ID);
                return RedirectToAction("Manage");
            }
        }

        //Displaying data according to parent/child in dropdown...
        //public static List<GetParentChildCategories_Result> CategoriesByParent()
        //{
        //    return new CategoryModel().ParentsChildCategories();
        //}

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(ProductPhoto model, List<HttpPostedFileBase> fileUpload)
        {
            // Your Code - / Save Model Details to DB

            // Handling Attachments -
            foreach (HttpPostedFileBase item in fileUpload)
            {
                if (Array.Exists(model.Thumbnail.Split(','), s => s.Equals(item.FileName)))
                {
                    //Save or do your action -  Each Attachment ( HttpPostedFileBase item )
                }
            }
            return View("Index");
        }
    }
}