using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Silkways.Models
{
    public class CategoryModel
    {
        protected GaintShoeBoxEntities db = new GaintShoeBoxEntities();

        public void Save(ProductCategory1 cate)
        {
            db.ProductCategories1.Add(cate);
            db.SaveChanges();
        }
        public void Update(ProductCategory1 cate)
        {
            db.ProductCategories1.Attach(cate);
            var update = db.Entry(cate);
            update.Property(x => x.SortNumber).IsModified = true;
            update.Property(x => x.CategoryName).IsModified = true;
            db.SaveChanges();
        }

        public List<ProductCategory1> GetAll()
        {
            return db.ProductCategories1.OrderByDescending(x => x.ID).ToList();
        }

        public ProductCategory1 GetbyID(int ID)
        {
            return db.ProductCategories1.OrderByDescending(x => x.ID).Where(x => x.ID == ID).FirstOrDefault();
        }

        public void Delete(int CategoryID)
        {
            var item = db.ProductCategories1.Where(x => x.ID == CategoryID).FirstOrDefault();
            db.ProductCategories1.Remove(item);
            db.SaveChanges();

        }

        //public List<GetParentChildCategories_Result> ParentsChildCategories()
        //{
        //    return db.GetParentChildCategories().ToList();
        //}

      

    }
}