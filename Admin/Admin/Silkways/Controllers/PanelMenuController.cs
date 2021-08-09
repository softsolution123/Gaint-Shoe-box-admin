using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class PanelMenuController : Controller
    {
        public ActionResult AddPanelMenus()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                PanelMenu Edit = new MenuPanelModel().GetMenuPanelByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPanelMenus(PanelMenu PanelMenus)
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                PanelMenus.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new MenuPanelModel().Update(PanelMenus);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new MenuPanelModel().Save(PanelMenus);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("ManagePanelMenus");
        }

        public ActionResult ManagePanelMenus()
        {
            List<PanelMenu> lst = new MenuPanelModel().GetAllMenuPanel();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new MenuPanelModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManagePanelMenus");
        }
	}
}