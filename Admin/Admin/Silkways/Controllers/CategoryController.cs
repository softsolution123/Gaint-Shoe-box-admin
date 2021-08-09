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
    public class CategoryController : Controller
    {
        public ActionResult AddCategory()
        {
            var lst = new CategoryModel().GetAll().OrderBy(x => x.ID);
          
          
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ProductCategory1 cate = new CategoryModel().GetbyID(ID);
                return View(cate);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddCategory(ProductCategory1 cate)
        {
            //var lst = new GernalFunction().CategoryList();
       
            
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                cate.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new CategoryModel().Update(cate);
                TempData["AlertTask"] = "Record Updated Successfully";
                return RedirectToAction("ManageCategory");
            }
            else
            {
                new CategoryModel().Save(cate);
                TempData["AlertTask"] = "Record Saved Successfully";
                return RedirectToAction("ManageCategory");
            }
            
    }

        public ActionResult ManageCategory()
        {
            List<ProductCategory1> cate = new CategoryModel().GetAll();
            return View(cate);
        }

        public ActionResult Delete(int ID)
        {
           
    
            {
                new CategoryModel().Delete(ID);
                return RedirectToAction("ManageCategory");
            }
        }

        //Displaying data according to parent/child in dropdown...
        public static List<ProductCategory1> GetCategories()
        {
            return  new CategoryModel().GetAll();
        }






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