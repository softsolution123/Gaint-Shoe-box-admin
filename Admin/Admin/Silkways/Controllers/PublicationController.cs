using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Silkways.Models;
using System.IO;
using Silkways.Models.SilkwaysFunction;
using Silkways.Models;

namespace Silkways.Controllers
{
    public class PublicationController : Controller
    {
        // GET: Product
        private GaintShoeBoxEntities db = new Models.GaintShoeBoxEntities();
        int CountingImages = 0;
        int CountingImagesPath = 0;
        string img = "";
        string DBimg = "";
        public ActionResult Add()
        {
          

            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Publication Edit = new PublicationModel().GetByID(ID);
                ViewBag.img2 += "<li><img src='/Resources/Publications/" + Edit.Image + "' value='" + Edit.ID + "'  width='76px'  height='54px' style='float: left; height:54px; width=76px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                ViewBag.img += "<li style='width:215px; height:215px;'><img src='https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/PDF_file_icon.svg/625px-PDF_file_icon.svg.png'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                return View(Edit);
            }
            else
            {

                Publication P = new Publication();
                return View(P);
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Publication publication, HttpPostedFileBase file, string Image , HttpPostedFileBase file2, string oldPDF)
        {
            var productID = 0;
            var count = 0;
            string[] newStr = new string[15];
            string[] data = new string[15];
            string[] filname = new string[15];
            string ImgName = "";
            string ImgPathName = "";

            if (file != null)
            {
                publication.Image = GernalFunction.SaveFile(file, Constants.PathPublications);
            }
            else if (!string.IsNullOrEmpty(Image))
            {
                publication.Image = Image;
            }
            else
            {
                publication.Image = Constants.NoImage;
            }



            if (file2 != null)
            {
                publication.PdfFile = GernalFunction.SaveFile(file2, Constants.PathPublications);
            }
            else if (!string.IsNullOrEmpty(oldPDF))
            {
                publication.PdfFile = oldPDF;
            }

            else
            {
                publication.PdfFile = Constants.NoImage;
            }


      
            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                publication.ID = Convert.ToInt16(Request.QueryString["ID"]);
                new PublicationModel().Update(publication);
                TempData["AlertTask"] = "Record Successfully Updated";
            }
            else
            {
                Publication P = new PublicationModel().Save(publication);
                if (P!=null && P.ID > 0)
                {
                    publication.ID = P.ID;
                }
                else
                {
                    TempData["AlertTask"] = "Record Successfully Updated";
                    return RedirectToAction("Manage");
                }
                
            }


            //if (file!=null && file.Length > 0)
            //{
            //   // DeleteImagesbyProductID(product.ID);
            //    for (int i = 0; i < file.Length; i++)
            //    {
            //        int counts = 1;

            //        if (file[i] != null)
            //        {
            //            ImgName = GernalFunction.SaveFile(file[i], Constants.PathProductsImages, counts + "-" + Convert.ToString(DateTime.Now.Ticks) + file[i].FileName.Substring(file[i].FileName.IndexOf(".")));
            //            imagesAlbum.ProductID = product.ID;
            //            imagesAlbum.LargePhoto = ImgName;
            //            imagesAlbum.MedPhoto = ImgName;
            //            imagesAlbum.Thumbnail = ImgName;
            //            new ProductImagesModel().Save(imagesAlbum);
            //            counts++;
            //            //do something for resizing
            //            //GernalFunction.ResizePicture3(198, 284, ResizePicturePath, ImgName, Constants.PathProductsImages, 0.5, item.InputStream);
            //        }

            //        TempData["AlertTask"] = "Record Successfully Saved";
            //    }
            //}
            
            return RedirectToAction("Manage");
        }

        public ActionResult Manage()
        {
            List<Publication> lst = new PublicationModel().GetAllPublications();
            return View(lst);
        }

        public ActionResult Delete(int ID)
        {
            Publication Edit = new PublicationModel().GetByID(ID);
            // new AlbumImagesModel().DeleteByAlbumID(ID);
            //var photoModel = db.ProductPhotos.Where(x => x.ProductID == ID).ToList();
            //foreach (var item in photoModel)
            //{

            //    string fullPath = Request.MapPath("~/Resources/ProductsImages/" + item.Thumbnail);
            //    if (System.IO.File.Exists(fullPath))
            //    {
            //        System.IO.File.Delete(fullPath);
            //    }
            //    string fullpth = Request.MapPath("~/Resources/ProductsImages/" + item.MedPhoto);
            //    if (System.IO.File.Exists(fullpth))
            //    {
            //        System.IO.File.Delete(fullpth);
            //    }
            //    string path = Request.MapPath("~/Resources/ProductsImages/" + item.LargePhoto);
            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }
            //}
            new PublicationModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            return RedirectToAction("Manage");
        }

        public static List<Category> GetCategories()
        {
            return new ProductModel().GetListOfCategories();
        }
        public bool DeleteAlbumImage(int id)
        {
            var photoModel = db.ProductPhotos.Where(x => x.PhotoID == id).SingleOrDefault();

            string fullPath = Request.MapPath("~/Resources/ProductsImages/" + photoModel.Thumbnail);
            System.IO.File.Delete(fullPath);

            string fullpth = Request.MapPath("~/Resources/ProductsImages/" + photoModel.MedPhoto);
            System.IO.File.Delete(fullpth);

            string path = Request.MapPath("~/Resources/ProductsImages/" + photoModel.LargePhoto);
            System.IO.File.Delete(path);

            new ProductImagesModel().DeleteAlbumImageById(id);
            TempData["tempId"] = "1";
            return true;
        }
        public void DeleteImagesbyProductID(int ID)
        {
            new ProductImagesModel().DeleteAlbumImageByProductID(ID);

        }
        public string CheckImageExists(string ImgName)
        {
            var count = db.ProductPhotos.Where(x => x.Thumbnail == ImgName).Count();
            CountingImages++;
            DBimg = ImgName;
            if (count > 0)
            {
                string[] NewImgName = ImgName.Split('.');
                DBimg = NewImgName[0] + CountingImages + "." + NewImgName[1];
                CheckImageExists(DBimg);
                return DBimg;

            }
            else
            {
                CountingImages = 0;
                return DBimg;
            }

        }
        public string CheckPathExists(string ImgName)
        {

            CountingImagesPath++;
            img = ImgName;
            if (System.IO.File.Exists(Server.MapPath("/Resources/ProductsImages/" + ImgName)))
            {
                string[] NewImgName = ImgName.Split('.');
                img = NewImgName[0] + CountingImagesPath + "." + NewImgName[1];
                CheckPathExists(img);
                return img;

            }
            else
            {
                CountingImagesPath = 0;
                return img;
            }

        }
    }
}