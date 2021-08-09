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
    public class SettingsController : Controller
    {
        public ActionResult AddSettings()
        {
            Setting S = new SettingsModel().GetDefaultSettings();
            return View(S);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddSettings(
            Setting sting, 
            HttpPostedFileBase Favicon,
            HttpPostedFileBase WebLogo,
            HttpPostedFileBase FooterLogo, HttpPostedFileBase file, string ImageName)
        {
           
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                 
                    sting.WebLogo = GernalFunction.SaveFile(file, Constants.WebLogos);
                  
                  
                    string ResizePicturePath = Constants.WebLogos;
                    string ResizePictureNewPath = Constants.WebLogos2;
                    GernalFunction.ResizePicture3(144, 240, ResizePicturePath, sting.WebLogo, ResizePictureNewPath,100, file.InputStream);
                }
            }
            else if (ImageName != null)
            {
                sting.WebLogo = Path.GetFileName(ImageName);
            }
            else
            {
                sting.WebLogo = Constants.NoImage;
            }
            if (sting!=null && sting.ID > 0)
            {
                new SettingsModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new SettingsModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("AddSettings");
        }
	}
}