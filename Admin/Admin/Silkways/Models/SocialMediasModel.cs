using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SocialMediasModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(SocialMedia sting)
        {
            try
            {
                db.SocialMedias.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(SocialMedia sting)
        {
            try
            {
                db.SocialMedias.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Title).IsModified = true;
                Update.Property(x => x.Value).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<SocialMedia> GetAllSocialMedias()
        {
            return db.SocialMedias.OrderByDescending(x => x.ID).ToList();
        }

        public SocialMedia GetSocialMediasByID(int ID)
        {
            return db.SocialMedias.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.SocialMedias.Where(x => x.ID == ID).FirstOrDefault();
            db.SocialMedias.Remove(item);
            db.SaveChanges();

        }
    }
}