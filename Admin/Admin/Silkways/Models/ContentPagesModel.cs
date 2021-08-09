using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class ContentPagesModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(ContentPage page)
        {
            try
            {
                db.ContentPages.Add(page);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(ContentPage pages)
        {
            try
            {
                db.ContentPages.Attach(pages);
                var Update = db.Entry(pages);
                Update.Property(x => x.Heading).IsModified = true;
                Update.Property(x => x.URL).IsModified = true;
                Update.Property(x => x.Description).IsModified = true;
                Update.Property(x => x.Date).IsModified = true;
                Update.Property(x => x.UserID).IsModified = true;
                Update.Property(x => x.MetaTitle).IsModified = true;
                Update.Property(x => x.MetaKeyword).IsModified = true;
                Update.Property(x => x.MetaDescription).IsModified = true;
                Update.Property(x => x.PageName).IsModified = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<ContentPage> GetAllContentUnit()
        {
            string isGuide = "true";

            return db.ContentPages.Where( x => x.IsGuide.Equals( "true" ) ).ToList() ;
        }

        public List<ContentPage> GetAllContentUnitReal()
        {
            string isGuide = "true";

            return db.ContentPages.Where( x => x.IsGuide != "true" ).ToList();
        }

        public ContentPage GetContentPageByID(int ID)
        {
            return db.ContentPages.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.ContentPages.Where(x => x.ID == ID).FirstOrDefault();
            db.ContentPages.Remove(item);
            db.SaveChanges();

        }
    }
}