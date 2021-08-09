using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;

namespace Silkways.Controllers
{
    public class InquiryController : Controller
    {
        // GET: Inquiry
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult ManageInquiry()
        {
            return View(db.ContactUs.ToList());
        }
        public void Delete(int ID)
        {
            var item = db.ContactUs.Where(x => x.ID == ID).FirstOrDefault();
            db.ContactUs.Remove(item);
            db.SaveChanges();
            TempData["AlertTask"] = "Record Successfully Deleted";
            Response.Redirect("ManageInquiry");
        }
    }
}