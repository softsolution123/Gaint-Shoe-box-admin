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
    public class SizeController : Controller
    {
        public ActionResult Add()
        {
    
          
          
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ProductSize cate = new SizeModel().GetbyID(ID);
                return View(cate);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Add(ProductSize cate)
        {
            //var lst = new GernalFunction().CategoryList();
       
            
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                cate.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new SizeModel().Update(cate);
                TempData["AlertTask"] = "Record Updated Successfully";
                return RedirectToAction("Manage");
            }
            else
            {
                new SizeModel().Save(cate);
                TempData["AlertTask"] = "Record Saved Successfully";
                return RedirectToAction("Manage");
            }
            
    }

        public ActionResult Manage()
        {
            List<ProductSize> cate = new SizeModel().GetAll();
            return View(cate);
        }


        public static List<ProductSize> GetSizes()
        {
            return new SizeModel().GetAll();
        }


        public ActionResult Delete(int ID)
        {
           
    
            {
                new SizeModel().Delete(ID);
                return RedirectToAction("Manage");
            }
        }

       
    }
}