using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using System.IO;
using Silkways.Models.SilkwaysFunction;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        private static GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public ActionResult AddGallery()
        {
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Gallery Edit = new GalleryModel().GetGalleryByID(ID);
                List<GalleryPhoto> lstImages = db.GalleryPhotos.Where(a => a.GalleryID == ID).ToList();
                foreach (var item in lstImages)
                {
                    ViewBag.img += "<li style='width:215px; height:215px;'><img src='" + Constants.PathGalleryImages + item.Image + "' value='" + item.ID + "'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + item.ID + " ' onclick='ImageRemove(this);' class='close'>X</span><input type='text' value='" + item.Image + "' style='display:none;' /></li>";
                }
                return View(Edit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddGallery(Gallery Gallery, HttpPostedFileBase[] file)
        {
            if (Gallery != null && Gallery.ID > 0)
            {
                new GalleryModel().Update(Gallery);
                TempData["AlertTask"] = "Record Successfully Updated";
            }
            else
            {
                Gallery G = new Gallery();
                G.VideoEmbed = Gallery.VideoEmbed;
                G.CategoryID = Gallery.CategoryID;
                G.SortNumber = Gallery.SortNumber;
                G.IsHome = Gallery.IsHome;
             
        
                db.Galleries.Add(G);
                db.SaveChanges();
                if (G != null && G.ID > 0)
                {
                    Gallery.ID = G.ID;
                }
            }
            if (file != null && file.Length > 0 && Gallery.ID > 0)
            {
                int count = 1;
                foreach (var item in file)
                {
                    if (item != null)
                    {
                        GalleryPhoto GP = new GalleryPhoto();
                        GP.GalleryID = Gallery.ID;
                        GP.Image = GernalFunction.SaveFile(item, Constants.PathGalleryImages, count+"-"+ Convert.ToString(DateTime.Now.Ticks) + item.FileName.Substring(item.FileName.IndexOf(".")));
                        db.GalleryPhotos.Add(GP);
                        db.SaveChanges();
                        string ResizePicturePath = Constants.PathGalleryImages;
                        string ResizePictureNewPath = Constants.PathGalleryImagesthumb;
                        GernalFunction.ResizePicture3(198, 284, ResizePicturePath, GP.Image, ResizePictureNewPath, 0.5, item.InputStream);
                    }
                    count++;
                }
            }
            TempData["AlertTask"] = "Record Successfully Saved";
            return RedirectToAction("ManageGallery");
        }

        public ActionResult ManageGallery()
        {
            List<Gallery> lst = new GalleryModel().GetAllGalleries();
            //GalleryListingViewModel lst = new GalleryListingViewModel();
            return View(lst);
        }
        public ActionResult Delete(int ID)
        {
            Gallery Edit = new GalleryModel().GetGalleryByID(ID);
            // new AlbumImagesModel().DeleteByAlbumID(ID);
            List<GalleryPhoto> photoModel = db.GalleryPhotos.Where(x => x.GalleryID == ID).ToList();

            //var item = db.Gallerys.Where(x => x.ID == id).SingleOrDefault();

            foreach (var item in photoModel)
            {
                db.GalleryPhotos.Remove(item);
                db.SaveChanges();
                string fullPath = Server.MapPath("/Resources/GallerysImages/" + item.Image);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }
            new GalleryModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            return RedirectToAction("ManageGallery");
        }
        public bool DeleteGalleryImage(int id)
        {
            var photoModel = db.GalleryPhotos.Where(x => x.ID == id).SingleOrDefault();

            string fullPath = Request.MapPath(Constants.PathGalleryImages + photoModel.Image);
            System.IO.File.Delete(fullPath);
            db.GalleryPhotos.Remove(photoModel);
            db.SaveChanges();
            TempData["tempId"] = "1";
            return true;
        }
    }
}