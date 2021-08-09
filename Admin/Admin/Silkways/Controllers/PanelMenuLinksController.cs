using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class PanelMenuLinksController : Controller
    {
        public ActionResult AddPanelMenuLinks(int PanelID, int? ID)
        {
            
            if (ID!=null && ID > 0)
            {
                int LinkID = Convert.ToInt32(ID);
                PanelMenuLink Edit = new PanelMenuLinksModel().GetPanelMenuLinksByID(LinkID);
                return View(Edit);
            }
            else
            {
                PanelMenuLink PM = new PanelMenuLink();
                PM.PanelMenuID = PanelID;
                return View(PM);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPanelMenuLinks(PanelMenuLink PanelMenuLinks)
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                PanelMenuLinks.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new PanelMenuLinksModel().Update(PanelMenuLinks);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new PanelMenuLinksModel().Save(PanelMenuLinks);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return Redirect("/PanelMenuLinks/ManagePanelMenuLinks?PanelID="+ PanelMenuLinks.PanelMenuID);
        }

        public ActionResult ManagePanelMenuLinks(int PanelID)
        {
            ViewBag.PanelID = PanelID;
            List<PanelMenuLink> lst = new PanelMenuLinksModel().GetAllPanelMenuByMainID(PanelID);
            return View(lst);
        }

        public void Delete(int ID)
        {
            new PanelMenuLinksModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManagePanelMenuLinks");
        }
	}
}