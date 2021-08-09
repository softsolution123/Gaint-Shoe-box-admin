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
    public class ProductController : Controller
    {
        // GET: Product

        private GaintShoeBoxEntities db = new Models.GaintShoeBoxEntities();
        int CountingImages = 0;
        int CountingImagesPath = 0;
        string img = "";
        string DBimg = "";
        public ActionResult AddProduct()
    
        {
            


            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                Product Edit = new ProductModel().GetProductByID(ID);
                ViewBag.img2 += "<li><img src='/Resources/ProductImages/" + Edit.ProductImage + "' value='" + Edit.ID + "'  width='76px'  height='54px' style='float: left; height:54px; width=76px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                ViewBag.img += "<li style='width:215px; height:215px;'><img src='https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/PDF_file_icon.svg/625px-PDF_file_icon.svg.png'  width='200px'  height='200px' style='float: left; height:200px; width=200px; margin: 0px; ' /><span value='" + Edit.ID + " ' onclick='ImageRemove(this);' class='close'>X</span></li>";
                return View(Edit);
            }
            else
            {

                Product P = new Product();
                return View(P);
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product product, HttpPostedFileBase[] images, string Image , string SizeID  , string   Category_Id)
        {
            var productID = 0;
            var count = 0;
            string[] newStr = new string[15];
            string[] data = new string[15];
            string[] filname = new string[15];
            string ImgName = "";
            string ImgPathName = "";




            string SelectedColors = Request.Form["Category_Id"];
            List<string> SelectedColorsList = new List<string>(SelectedColors.Split(','));



            List<int> ProductColorIDList = new List<int>();
            if (SelectedColorsList!=null)
            {
                
                foreach (var item in SelectedColorsList)
                {
                    if (item == "")
                    {
                        break;
                    }
                    int id = 0;
                    //ProductColorID = Category_Id.Split(',');
                    id = Convert.ToInt32(item);
                    ProductColorIDList.Add(id);
                }
                //ProdFeatureIdArray=ProdFeatureIdArray.Distinct();

            }



            List<int> ProductSizeIDList = new List<int>();
            if (!String.IsNullOrEmpty(SizeID))
            {
                var ProductSizeID = SizeID.Split(',');
                foreach (var item in ProductSizeID)
                {
                    if (item == "")
                    {
                        break;
                    }
                    int id = 0;
                    id = Convert.ToInt32(item);
                    ProductSizeIDList.Add(id);
                }
                //ProdFeatureIdArray=ProdFeatureIdArray.Distinct();

            }





            //List<int> ProductColorIDList = new List<int>();
            //if (!String.IsNullOrEmpty(Category_Id))
            //{
            //    var ProductColorID = Category_Id.Split(',');
            //    foreach (var item in ProductColorID)
            //    {
            //        if (item == "")
            //        {
            //            break;
            //        }
            //        int id = 0;
            //        id = Convert.ToInt32(item);
            //        ProductColorIDList.Add(id);
            //    }
               

            //}
            //string SelectedSizes = Request.Form["SizeID"];
            //string SelectedSizes = Request.Form["SizeID"];

            //int Color = Category_Id;

            ProductPhoto imagesAlbum = new ProductPhoto();


            List<ProductPhoto> product_images = new List<ProductPhoto>();
            if (images != null && images.Count() > 0)
            {
                foreach (var item in images)
                {
                    //Thread.Sleep(20);
                    if (item != null)
                    {

                        string ext = Path.GetExtension(item.FileName);
                        string FileName = DateTime.Now.ToString("fffffffddyyMMHHmmss") + ext;
                        string path = Server.MapPath("/Resources/ProductsImages/" + FileName);
                        item.SaveAs(path);
                        ProductPhoto productimages = new ProductPhoto();
                        productimages.Thumbnail = FileName;
                        product_images.Add(productimages);
                    }
                }
            }


            //List<ProductSize> product_sizes = new List<ProductSize>();

            //for (int i = 0; i < ProductSizeIdArray.Count; i++)
            //{

            //    if (obj.ID != 0)
            //    {
            //        product_sizes.Add(new ProductSize { SizeID = ProductSizeIdArray[i], Quantity = ProdFeatureIdArray[i], ProductID = obj.ID });

            //    }

            //    else
            //    {


            //        product_sizes.Add(new ProductSize { SizeID = ProductSizeIdArray[i], Quantity = ProdFeatureIdArray[i], ProductID = obj.ID });


            //    }

            //}

            if (Convert.ToInt32(Request.QueryString["ID"]) != 0 && Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                product.ID = Convert.ToInt16(Request.QueryString["ID"]);
                TempData["AlertTask"] = "Record Updated Successfully";
            }
            else
            {
                TempData["AlertTask"] = "Record Saved Successfully";
            }
            //new ProductModel().AddProduct(obj, product_images );

            new ProductModel().Update(product, product_images, ProductSizeIDList, ProductColorIDList);

            //if (file != null && file.Length > 0)
            //{
            //    // DeleteImagesbyProductID(product.ID);
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

            return RedirectToAction("ManageProduct");
        }

        public ActionResult ManageProduct()
        {
            List<Product> lst = new ProductModel().GetAllProducts();
            return View(lst);
        }

        public ActionResult Delete(int ID)
        {
            Product Edit = new ProductModel().GetProductByID(ID);
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
            new ProductModel().Delete(ID);
            TempData["AlertTask"] = "Record Successfully Delete";
            return RedirectToAction("ManageProduct");
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




        public void DeleteProductImageByImageID(int ID)
        {
            new ProductImagesModel().DeleteImageById(ID);

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