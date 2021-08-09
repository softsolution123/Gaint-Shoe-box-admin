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
    public class ProductBrandController : Controller
    {
        // GET: ProductBrand
        public GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public ActionResult ManageBrands()
        {
            var brand = new BrandModel().GetAllBrands();
            return View(brand);
        }
        [HttpGet]
        public ActionResult AddBrand()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ProductBrand Edit = new BrandModel().GetAllBrandByID(ID);
                ViewBag.img += "<li style='width:215px; height:215px;'><img src='" +Constants.PathBrandsImages + Edit.BrandImage + "' value='" + Edit.ID + "'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                // ViewBag.img += "<li><img src='/Resources/Brand/" + Edit.BrandImage + "' value='" + Edit.ID + "'  width='76px'  height='54px' style='float: left; height:54px; width=76px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBrand(ProductBrand brand, HttpPostedFileBase file, string CheckBox)
        {


            if (file != null)
            {
                brand.BrandImage = GernalFunction.SaveFile(file, Constants.PathBrandsImages);
            }
            else
            {
                brand.BrandImage = Constants.NoImage;
            }
            if (CheckBox == null || CheckBox == "")
            {
                brand.IsShowHome = false;
            }
            else
            {
                brand.IsShowHome = true;
            }

            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            if (ID == 0)
            {
                new BrandModel().Save(brand);
                return RedirectToAction("ManageBrands");
            }
            else
            {
                new BrandModel().Update(brand);
                return RedirectToAction("ManageBrands");
            }
        }
        public ActionResult Delete()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                new BrandModel().Delete(ID);
                return RedirectToAction("ManageBrands");
            }
            else
            {
                return RedirectToAction("ManageBrands");
            }
        }

        public List<ProductBrand> GetBrands()
        {
            return db.ProductBrands.ToList();
        }
    }
}