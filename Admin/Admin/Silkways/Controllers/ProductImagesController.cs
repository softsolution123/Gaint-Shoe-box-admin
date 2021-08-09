using Silkways.Models.SilkwaysFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using System.IO;
using System.Drawing;
using ImagerLib;
using System.Web.Helpers;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Silkways.Controllers
{
    public class ProductImagesController : Controller
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult addProductImages()
        {
            var lst = db.Products.ToList();
            ViewBag.ProductsList = lst;
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Product Edit = new ProductModel().GetProductByID(ID);
                List<ProductPhoto> lstImages = new ProductImagesModel().GetProductImageByProductID(ID);
                if (lst.Count() > 0)
                {
                    foreach (var item in lstImages)
                    {
                        ViewBag.img += "<li><img src='" +Constants.PathProductsImages + item.Thumbnail + "' value='" + item.PhotoID + "'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + item.PhotoID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                    }
                }
                ViewBag.ProductName = Edit.ProductName;
                ViewBag.AlbumID = Edit.ID;
                return View(Edit);
            }
            else if (TempData["tempId"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Product Edit = new ProductModel().GetProductByID(ID);
                List<ProductPhoto> lstImages = new ProductImagesModel().GetProductImageByProductID(ID); ;
                if (lst.Count() > 0)
                {
                    foreach (var item in lstImages)
                    {
                        ViewBag.img += "<li><img src='" + Constants.PathProductsImages + item.Thumbnail + "' value='" + item.PhotoID + "'  width='76px'  height='54px' style='float: left; height:54px; width=76px; margin: 0px; ' /><span value='" + item.PhotoID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                    }
                }
                ViewBag.AlbumName = Edit.ProductName;
                ViewBag.AlbumID = Edit.ID;
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult addProductImages(ProductPhoto imagesAlbum, HttpPostedFileBase file, int ProductID)
        {
            var lst = db.Products.ToList();
            ViewBag.ProductsList = lst;
            if (file != null)
            {
                string FileName = GernalFunction.SaveFile(file, Constants.PathProductsImages);
                imagesAlbum.ProductID = ProductID;
                imagesAlbum.LargePhoto = FileName;
                imagesAlbum.MedPhoto = FileName;
                imagesAlbum.Thumbnail = FileName;
                new ProductImagesModel().Save(imagesAlbum);

            }
            return RedirectToAction("ManageProduct", "Product");
        }      

    }
}