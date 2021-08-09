using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SettingsModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(Setting sting)
        {
            try
            {
                db.Settings.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(Setting sting)
        {
            try
            {
                db.Settings.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Title).IsModified = true;
                Update.Property(x => x.Email).IsModified = true;
                Update.Property(x => x.Mobile).IsModified = true;
                Update.Property(x => x.Phone).IsModified = true;
                Update.Property(x => x.Address).IsModified = true;
                Update.Property(x => x.Timing).IsModified = true;
                Update.Property(x => x.HomePageContent).IsModified = true;
                Update.Property(x => x.HomePageMetatTitle).IsModified = true;
                Update.Property(x => x.Client).IsModified = true;
                Update.Property(x => x.HomePageMetatDescription).IsModified = true;
                Update.Property(x => x.HomePageMetatKeyword).IsModified = true;
                if (!string.IsNullOrEmpty(sting.WebFavicon))
                {
                    Update.Property(x => x.WebFavicon).IsModified = true;
                }
                if (!string.IsNullOrEmpty(sting.WebLogo))
                {
                    Update.Property(x => x.WebLogo).IsModified = true;
                }
                if (!string.IsNullOrEmpty(sting.FooterLogo))
                {
                    Update.Property(x => x.FooterLogo).IsModified = true;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<Setting> GetAllSettings()
        {
            return db.Settings.OrderByDescending(x => x.ID).ToList();
        }
        public Setting GetDefaultSettings()
        {
            return db.Settings.FirstOrDefault();
        }
        public Setting GetSettingsByID(int ID)
        {
            return db.Settings.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.Settings.Where(x => x.ID == ID).FirstOrDefault();
            db.Settings.Remove(item);
            db.SaveChanges();

        }
    }
}