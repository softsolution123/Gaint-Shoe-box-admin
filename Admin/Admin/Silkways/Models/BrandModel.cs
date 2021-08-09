using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;

namespace Silkways.Models
{
    public class BrandModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save(ProductBrand brand)
        {
            if (brand != null)
            {
                try
                {
                    db.ProductBrands.Add(brand);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void Update(ProductBrand brand)
        {
            try
            {
                db.ProductBrands.Attach(brand);
                var update = db.Entry(brand);
                update.Property(x => x.BrandName).IsModified = true;
                update.Property(x => x.Description).IsModified = true;
                update.Property(x => x.BrandImage).IsModified = true;
                update.Property(x => x.IsShowHome).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public ProductBrand GetAllBrandByID(int ID)
        {
            return db.ProductBrands.Find(ID);
        }
        public List<ProductBrand> GetAllBrands()
        {
            return db.ProductBrands.ToList();
        }
        public void Delete(int ID)
        {
            var brand = db.ProductBrands.Where(x => x.ID == ID).SingleOrDefault();
            db.ProductBrands.Remove(brand);
            db.SaveChanges();
        }
    }
}