using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult AddAboutUs()
        {

            AboutU lst = new AboutUsModel().GetDefault();
            return View(lst);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAboutUs(AboutU abt, HttpPostedFileBase file , HttpPostedFileBase file2)
        {
            if (file != null)
            {
                if (file != null)
                {
                    abt.Image1 = GernalFunction.SaveFile(file, Constants.PathAboutImage);
                }
                else
                {
                    abt.Image1 = Constants.NoImage;
                }
            }


            if (abt.ID != 0)
            {
                abt.ID = abt.ID;
                new AboutUsModel().Update(abt);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new AboutUsModel().Save(abt);
            }
            return RedirectToAction("AddAboutUs");
        }
    }
}