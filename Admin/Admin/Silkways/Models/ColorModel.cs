using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Silkways.Models;

namespace Silkways.Models
{
    public class ColorModel
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(tblColor color)
        {

           
            Type colorType = typeof(System.Drawing.Color);
            // We take only static property to avoid properties like Name, IsSystemColor ...
            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo propInfo in propInfos)
            {
                tblColor colors = new tblColor();
                colors.ColorName = propInfo.Name;
                colors.ColorValue = propInfo.Name;
                db.tblColors.Add(colors);
            }
            db.SaveChanges();

            //db.tblColors.Add(color);
            //db.SaveChanges();

            //System.Drawing.Color c = System.Drawing.Color.FromArgb(int);

            //int x = c.ToArgb();


        }
        public void Update(tblColor color)
        {
            db.tblColors.Attach(color);
            var update = db.Entry(color);
            update.Property(x => x.ColorName).IsModified = true;
            update.Property(x => x.ColorValue).IsModified = true;
            update.Property(x => x.CompanyColorValue).IsModified = true;
            db.SaveChanges();
        }

        public List<tblColor> GetAllColors()
        {
            return db.tblColors.OrderByDescending(x => x.ID).ToList();
        }

        public tblColor GetbyID(int ID)
        {
            return db.tblColors.Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ColorID)
        {
            var item = db.tblColors.Where(x => x.ID ==ColorID ).FirstOrDefault();
            db.tblColors.Remove(item);
            db.SaveChanges();

        }
        public bool DeleteColorProduct(int ID)
        {
            if (db.tblProductColors.Where(x => x.ColorID == ID).ToList().Count > 0)
            {
                return false;
            }
            else if (db.OrderDetails.Where(x => x.Color_id == ID).ToList().Count > 0)
            {
                return false;
            }
            else
            {
                var item = db.tblProductColors.Where(x => x.ColorID == ID).ToList();
                db.tblProductColors.RemoveRange(item);
                db.SaveChanges();
                return true;
            }
        }
    }
}