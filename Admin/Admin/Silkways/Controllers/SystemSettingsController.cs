using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class SystemSettingsController : Controller
    {
        public ActionResult AddSystemSettings()
        {
            SystemSetting S = new SystemSettingsModel().GetDefaultSystemSettings();
            return View(S);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSystemSettings(SystemSetting sting)
        {
            if (sting!=null && sting.ID > 0)
            {
                new SystemSettingsModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new SystemSettingsModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("AddSystemSettings");
        }
	}
}