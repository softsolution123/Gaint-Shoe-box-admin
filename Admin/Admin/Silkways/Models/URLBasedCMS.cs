using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;

namespace Silkways.Models
{
    public class URLBasedCMS
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(URLBasedCMSPage page)
        {
            try
            {
                db.URLBasedCMSPages.Add(page);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(URLBasedCMSPage page)
        {
            try
            {
                db.URLBasedCMSPages.Attach(page);
                var Update = db.Entry(page);
                Update.Property(x => x.Heading).IsModified = true;
                Update.Property(x => x.URL).IsModified = true;
                Update.Property(x => x.Description).IsModified = true;
                Update.Property(x => x.Date).IsModified = true;
                Update.Property(x => x.UserID).IsModified = true;
                Update.Property(x => x.MetaTitle).IsModified = true;
                Update.Property(x => x.MetaKeyword).IsModified = true;
                Update.Property(x => x.MetaDescription).IsModified = true;
                Update.Property(x => x.PageName).IsModified = true;
                if (page.Pageimage != null)
                {
                    Update.Property(x => x.Pageimage).IsModified = true;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void ImageUpdate(URLBasedCMSPage page)
        {
            try
            {
                db.URLBasedCMSPages.Attach(page);
                var Update = db.Entry(page);
                Update.Property(x => x.Pageimage).IsModified = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public List<ContentPage> GetAllContentUnit()
        {
            string isGuide = "true";

            return db.ContentPages.Where(x => x.IsGuide.Equals("true")).ToList();
        }

        public List<URLBasedCMSPage> GetAllCMSPages()
        {
            return db.URLBasedCMSPages.ToList();
        }

        public URLBasedCMSPage GetCMSPageByID(int ID)
        {
            return db.URLBasedCMSPages.Where(x => x.ID == ID).FirstOrDefault();
        }

        public URLBasedCMSPage GetCMSPageByURL(string url)
        {
            return db.URLBasedCMSPages.Where(x => x.URL == url).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.URLBasedCMSPages.Where(x => x.ID == ID).FirstOrDefault();
            db.URLBasedCMSPages.Remove(item);
            db.SaveChanges();

        }
    }
}