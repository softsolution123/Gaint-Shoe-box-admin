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
    public class PromoController : Controller
    {
        public ActionResult AddPromoCode()
        {
            var lst = new PromoModel().GetPromoCodes().OrderBy(x => x.ID);
          
          
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                tblPromoCode promo = new PromoModel().GetbyID(ID);
                return View(promo);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddPromoCode(tblPromoCode promoCode)
        {
            //var lst = new GernalFunction().CategoryList();
       
            
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                promoCode.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new PromoModel().Update(promoCode);
                TempData["AlertTask"] = "Record Updated Successfully";
                return RedirectToAction("ManageCategory");
            }
            else
            {
                new PromoModel().Save(promoCode);
                TempData["AlertTask"] = "Record Saved Successfully";
                return RedirectToAction("ManageCategory");
            }
            
    }

        public ActionResult ManagePromoCode()
        {
            List<tblPromoCode> PromoCodes = new PromoModel().GetPromoCodes();
            return View(PromoCodes);
        }

        public ActionResult Delete(int ID)
        {
           
    
            {
                new PromoModel().Delete(ID);
                return RedirectToAction("ManagePromoCode");
            }
        }

        //Displaying data according to parent/child in dropdown...
        //public static List<ProductCategory1> GetCategories()
        //{
        //    return  new CategoryModel().GetAll();
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