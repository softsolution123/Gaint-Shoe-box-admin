using Silkways.Models;
using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Silkways.Controllers
{
    public class TestimonialsController : Controller
    {
        public ActionResult AddTestimonials()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Testimonial Edit = new TestimonialModel().GetTestimonialsByID(ID);
                Edit.Image = Constants.PathTestimonialImage + Edit.Image;
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTestimonials(Testimonial sting, HttpPostedFileBase file)
        {
            if (file != null)
            {
                sting.Image = GernalFunction.SaveFile(file, Constants.PathTestimonialImage);
            }
            else if (!string.IsNullOrEmpty(sting.Image))
            {
                sting.Image = sting.Image;
            }
            else
            {
                sting.Image = Constants.NoImage;
            }
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                sting.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new TestimonialModel().Update(sting);
                TempData["AlertTask"] = "Record Successfully Update";
            }
            else
            {
                new TestimonialModel().Save(sting);
                TempData["AlertTask"] = "Record Successfully Saved";
            }
            return RedirectToAction("ManageTestimonials");
        }

        public ActionResult ManageTestimonials()
        {
            List<Testimonial> lst = new TestimonialModel().GetAllTestimonials();
            return View(lst);
        }

        public void Delete(int ID)
        {
            new TestimonialModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            Response.Redirect("ManageTestimonials");
        }
    }
}