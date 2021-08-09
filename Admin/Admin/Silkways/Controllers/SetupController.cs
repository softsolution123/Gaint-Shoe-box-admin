using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;

namespace Silkways.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        public ActionResult Step1()
        {
            return View();
        }
        public ActionResult Step2()
        {
            User U = new UserModel().GetMasterAdmin();
            return View(U);
        }
        [HttpPost]
        public ActionResult Step2(User Model)
        {
            Model.Image = "NoImage.jpg";
            Model.IsMasterAdmin = true;
            Model.IsAdmin = true;
            Model.IsActive = true;
            User U = new UserModel().GetMasterAdmin();            
            if (Model != null && Model.ID > 0)
            {
                new UserModel().Update(Model);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new UserModel().Save(Model);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("Step3");
        }
        public ActionResult Step3()
        {
            User U = new UserModel().GetMasterAdmin();
            if (U != null && U.ID > 0)
            {
                ViewBag.UserID = U.ID;
                List<PanelMenu> ListPanelMenu = new MenuPanelModel().GetAllMenuPanel();
                ListPanelMenu.ForEach(a => { a.IsActive = false; });
                List<PanelMenu> Selected = new MenuPanelModel().GetSelectedMenuPanelByUserID(U.ID);
                if (Selected != null)
                {
                    foreach (var item in Selected)
                    {
                        if (ListPanelMenu.FirstOrDefault(a => a.ID == item.ID) != null)
                        {
                            ListPanelMenu.FirstOrDefault(a => a.ID == item.ID).IsActive = true;
                        }

                    }
                }
                return View(ListPanelMenu);
            }
            else
            {
                return RedirectToAction("Step2");
            }

        }
        [HttpPost]
        public ActionResult Step3(int adminuser, string[] panel)
        {

            new MenuPanelModel().RemoveAllPanelRelationByUserID(adminuser);
            if (panel!=null && panel.Length > 0)
            {
                foreach (var item in panel)
                {
                    new MenuPanelModel().SavePanelRelation(adminuser, Convert.ToInt32(item));
                }
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("Step4");
        }
        public ActionResult Step4()
        {
            SystemSetting S = new SystemSettingsModel().GetDefaultSystemSettings();
            return View(S);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Step4(SystemSetting sting)
        {
            if (sting != null && sting.ID > 0)
            {
                new SystemSettingsModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new SystemSettingsModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return Redirect("/user/login");
        }
    }
}