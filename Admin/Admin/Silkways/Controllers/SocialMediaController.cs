using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class SocialMediaController : Controller
    {
        public ActionResult AddSocialMedias()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                SocialMedia Edit = new SocialMediasModel().GetSocialMediasByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSocialMedias(SocialMedia sting)
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                sting.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new SocialMediasModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new SocialMediasModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("ManageSocialMedias");
        }

        public ActionResult ManageSocialMedias()
        {
            List<SocialMedia> lst = new SocialMediasModel().GetAllSocialMedias();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new SocialMediasModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManageSocialMedias");
        }
	}
}