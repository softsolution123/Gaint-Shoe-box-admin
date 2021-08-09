using Silkways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class FAQsController : Controller
    {
        public ActionResult AddFAQs()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                FAQ Edit = new FAQsModel().GetfaqsByID(ID);
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddFAQs(FAQ faqs)
        {
            if (faqs.Decsription != null && faqs.Decsription != "")
            {
                if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
                {
                    faqs.ID = Convert.ToInt16(Request.QueryString["ID"]);
                    new FAQsModel().Update(faqs);
                    TempData["AlertTask"] = "Record Successfully Update";
                }
                else
                {
                    new FAQsModel().Save(faqs);
                    TempData["AlertTask"] = "Record Successfully Saved";
                }
                return RedirectToAction("ManageFAQs");
            }
            else
            {
                TempData["AlertTask"] = "Please Enter the Description";
                return View();
            }
        }

        public ActionResult ManageFAQs()
        {
            List<FAQ> lst = new FAQsModel().GetAllFaqs();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new FAQsModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManageFAQs");
        }
	}
}