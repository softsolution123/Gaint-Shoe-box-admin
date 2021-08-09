using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class GalleryCategoryModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(GalleryCategory sting)
        {
            try
            {
                db.GalleryCategories.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(GalleryCategory sting)
        {
            try
            {
                db.GalleryCategories.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Name).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<GalleryCategory> GetAllGalleryCategorys()
        {
            return db.GalleryCategories.OrderByDescending(x => x.ID).ToList();
        }

        public GalleryCategory GetGalleryCategorysByID(int ID)
        {
            return db.GalleryCategories.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.GalleryCategories.Where(x => x.ID == ID).FirstOrDefault();
            db.GalleryCategories.Remove(item);
            db.SaveChanges();

        }
    }
}