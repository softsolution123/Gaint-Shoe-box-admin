using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class AboutUsModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save(AboutU abt)
        {
            try
            {
                db.AboutUs.Add(abt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void Update(AboutU abt)
        {
            try
            {
                AboutU ab = db.AboutUs.FirstOrDefault(a => a.ID == abt.ID);
                ab.PageHeading = abt.PageHeading;
                ab.MetaTitle = abt.MetaTitle;
                ab.MetaKeyword = abt.MetaKeyword;
                ab.MetaDescription =  abt.MetaDescription;
                ab.Descriptions = abt.Descriptions;
                abt.Desciption1 = abt.Desciption1;

                if (abt.Image1 != null)
                {
                    ab.Image1 = abt.Image1;
                }
             


                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public List<AboutU> GetAllAbout()
        {
            return db.AboutUs.OrderByDescending(x => x.ID).ToList();
        }
        public AboutU GetAboutByID(int ID)
        {
            return db.AboutUs.Where(x => x.ID == ID).FirstOrDefault();
        }
        public AboutU GetDefault()
        {
            return db.AboutUs.FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.AboutUs.Where(x => x.ID == ID).FirstOrDefault();
            db.AboutUs.Remove(item);
            db.SaveChanges();

        }
    }
}