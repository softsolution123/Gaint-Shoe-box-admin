using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class ProductImagesModel
    {
            GaintShoeBoxEntities db = new GaintShoeBoxEntities();
            public void Save(ProductPhoto imges)
            {
                try
                {
                    db.ProductPhotos.Add(imges);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            }

            public void Update(ProductPhoto imges)
            {
                try
                {
                    db.ProductPhotos.Attach(imges);
                    var Update = db.Entry(imges);
                    Update.Property(x => x.Thumbnail).IsModified = true;
                    Update.Property(x => x.MedPhoto).IsModified = true;
                    Update.Property(x => x.LargePhoto).IsModified = true;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }





        public void DeleteImageById(int ID)
        {
            var ProductImage = db.ProductPhotos.Where(x => x.PhotoID == ID).SingleOrDefault();
            db.ProductPhotos.Attach(ProductImage);
            db.ProductPhotos.Remove(ProductImage);
            db.SaveChanges();
        }


        public List<ProductPhoto> GetAllProductImage()
            {
                return db.ProductPhotos.OrderByDescending(x => x.PhotoID).ToList();
            }

            public ProductPhoto GetAlbumImageByID(int ID)
            {
                return db.ProductPhotos.Where(x => x.PhotoID == ID).FirstOrDefault();
            }

            public void Delete(int ID)
            {
                var item = db.ProductPhotos.Where(x => x.PhotoID == ID).FirstOrDefault();
                db.ProductPhotos.Remove(item);
                db.SaveChanges();

            }

            public void DeleteByProductID(int ID)
            {
                var itemDel = db.ProductPhotos.Where(x => x.PhotoID == ID).ToList();
                foreach (var item in itemDel)
                {
                    db.ProductPhotos.Remove(item);
                    db.SaveChanges();
                }
            }
            public void DeleteAlbumImageById(int ID)
            {
                var ProductImage = db.ProductPhotos.Where(x => x.PhotoID == ID).SingleOrDefault();
                db.ProductPhotos.Attach(ProductImage);
                db.ProductPhotos.Remove(ProductImage);
                db.SaveChanges();
            }

            public void DeleteAlbumImageByProductID(int ID)
            {
                var ProductImage = db.ProductPhotos.Where(x => x.ProductID == ID).ToList();
                db.ProductPhotos.RemoveRange(ProductImage);
                db.SaveChanges();
            }
            public List<ProductPhoto> GetProductImageByProductID(int ProductID)
            {
                return db.ProductPhotos.Where(x => x.ProductID == ProductID).ToList();
            }
    }
}