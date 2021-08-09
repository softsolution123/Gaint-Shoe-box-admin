using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class SizeModel
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(ProductSize size)
        {
            db.ProductSizes.Add(size);
            db.SaveChanges();
        }
        public void Update(ProductSize size)
        {
            db.ProductSizes.Attach(size);
            var update = db.Entry(size);
            update.Property(x => x.SortNumber).IsModified = true;
            update.Property(x => x.Size).IsModified = true;
            db.SaveChanges();
        }

        public List<ProductSize> GetAll()
        {
            return db.ProductSizes.OrderByDescending(x => x.ID).ToList();
        }

        public ProductSize GetbyID(int ID)
        {
            return db.ProductSizes.OrderByDescending(x => x.ID).Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int ID)
        {
            var item = db.ProductSizes.Where(x => x.ID == ID).FirstOrDefault();
            db.ProductSizes.Remove(item);
            db.SaveChanges();

        }

      
    }
}