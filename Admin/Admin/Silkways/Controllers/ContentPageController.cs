using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class ContentPageController : Controller
    {
        public ActionResult AddContentPage()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ContentPage Edit = new ContentPagesModel().GetContentPageByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddContentPage(ContentPage page, string hdn_UserID)
        {
            if (page.Description != "" && page.Description != null)
            {
                page.Date = DateTime.Now;
                page.UserID = Convert.ToInt32(hdn_UserID);
                if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
                {
                    page.ID = Convert.ToInt32(Request.QueryString["ID"]);
                    new ContentPagesModel().Update(page);
                    TempData["AlertTask"] = "Record Successfully Update";
                }
                else
                {
                    new ContentPagesModel().Save(page);
                    TempData["AlertTask"] = "Record Successfully Saved";
                }
                return RedirectToAction("ManageContentPage");
            }
            else
            {
                TempData["AlertTask"] = "Please Enter the Description";
                return View();
            }
        }

        public ActionResult ManageContentPage()
        {
            List<ContentPage> lst = new ContentPagesModel().GetAllContentUnitReal();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new ContentPagesModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("ManageContentPage");
        }
	}
}