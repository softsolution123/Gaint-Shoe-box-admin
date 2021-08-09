using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Models
{
    public class GalleryModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public Gallery Save(Gallery Gallery)
        {
            try
            {
                db.Galleries.Add(Gallery);
                db.SaveChanges();
                return Gallery;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Gallery Gallery)
        {
            try
            {
                db.Galleries.Attach(Gallery);
                var update = db.Entry(Gallery);
                update.Property(x => x.IsHome).IsModified = true;
                update.Property(x => x.VideoEmbed).IsModified = true;
                update.Property(x => x.CategoryID).IsModified = true;
                update.Property(x => x.SortNumber).IsModified = true;
                update.Property(x => x.CategoryID).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public Gallery GetGalleryByID(int id)
        {
            return db.Galleries.Find(id);
        }

        public List<Gallery> GetAllGalleries()
        {
            return db.Galleries.ToList();
        }
        public void Delete(int id)
        {
            var item = db.Galleries.Where(x => x.ID == id).FirstOrDefault();
            if (item != null)
            {
                db.Galleries.Remove(item);
                db.SaveChanges();
            }
            
        }        
    }
}