using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class ProductColors
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(tblProductColor color)
        {
            db.tblProductColors.Add(color);
            db.SaveChanges();
        }
        public void Delete(int ID)
        {
            List<tblProductColor> item = db.tblProductColors.Where(x => x.ProductID == ID).ToList();
            db.tblProductColors.RemoveRange(item);
            db.SaveChanges();

        }
    }
}