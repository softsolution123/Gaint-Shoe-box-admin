using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class GalleryCategoryController : Controller
    {
        public ActionResult AddGalleryCategory()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                GalleryCategory Edit = new GalleryCategoryModel().GetGalleryCategorysByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddGalleryCategory(GalleryCategory sting)
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                sting.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new GalleryCategoryModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new GalleryCategoryModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("ManageGalleryCategory");
        }

        public ActionResult ManageGalleryCategory()
        {
            List<GalleryCategory> lst = new GalleryCategoryModel().GetAllGalleryCategorys();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new GalleryCategoryModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManageGalleryCategory");
        }
    }
}