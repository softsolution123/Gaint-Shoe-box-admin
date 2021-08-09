using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class PanelMenuLinksModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(PanelMenuLink sting)
        {
            try
            {
                db.PanelMenuLinks.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(PanelMenuLink sting)
        {
            try
            {
                db.PanelMenuLinks.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Title).IsModified = true;
                Update.Property(x => x.Link).IsModified = true;
                Update.Property(x => x.PanelMenuID).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<PanelMenuLink> GetAllPanelMenuLinks()
        {
            return db.PanelMenuLinks.OrderByDescending(x => x.ID).ToList();
        }
        public List<PanelMenuLink> GetAllPanelMenuByMainID(int ID)
        {
            return db.PanelMenuLinks.Where(a=>a.PanelMenuID == ID).OrderBy(x => x.Title).ToList();
        }
        public PanelMenuLink GetPanelMenuLinksByID(int ID)
        {
            return db.PanelMenuLinks.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.PanelMenuLinks.Where(x => x.ID == ID).FirstOrDefault();
            db.PanelMenuLinks.Remove(item);
            db.SaveChanges();

        }
    }
}