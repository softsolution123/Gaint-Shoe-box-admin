using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class MenuPanelModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(PanelMenu sting)
        {
            try
            {
                db.PanelMenus.Add(sting);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void Update(PanelMenu sting)
        {
            try
            {
                db.PanelMenus.Attach(sting);
                var Update = db.Entry(sting);
                Update.Property(x => x.Name).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public List<PanelMenu> GetAllMenuPanel()
        {
            return db.PanelMenus.Where(a => a.IsActive).OrderBy(x => x.Name).ToList();
        }
        public List<PanelMenu> GetMenuPanelByMasterAdmin(int ID)
        {
            return db.PanelMenuUserRelations.Where(a=>a.UserID == ID).Select(a=>a.PanelMenu).Distinct().ToList();
        }
        public List<PanelMenu> GetSelectedMenuPanelByUserID(int ID)
        {
            List<PanelMenu> ListMenu = db.PanelMenuUserRelations.Where(a => a.UserID == ID).Select(a=>a.PanelMenu).Distinct().ToList();
            return ListMenu;
        }
        public PanelMenu GetMenuPanelByID(int ID)
        {
            return db.PanelMenus.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.PanelMenus.Where(x => x.ID == ID).FirstOrDefault();
            db.PanelMenus.Remove(item);
            db.SaveChanges();

        }


        public void SavePanelRelation(int UserID, int PanelID)
        {
            try
            {
                PanelMenuUserRelation MPM = new PanelMenuUserRelation();
                MPM.PanelMenuID = PanelID;
                MPM.IsActive = true;
                MPM.UserID = UserID;
                db.PanelMenuUserRelations.Add(MPM);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void RemoveAllPanelRelationByUserID(int UserID)
        {
            try
            {
                List<PanelMenuUserRelation> ListPMUR = db.PanelMenuUserRelations.Where(a => a.UserID == UserID).ToList();
                if (ListPMUR!=null && ListPMUR.Count > 0)
                {
                    db.PanelMenuUserRelations.RemoveRange(ListPMUR);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}