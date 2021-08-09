using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Models
{
    public class ServiceModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save(Service Service)
        {
            try
            {
                db.Services.Add(Service);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void Update(Service Service)
        {
            try
            {
                db.Services.Attach(Service);
                var update = db.Entry(Service);
                update.Property(x => x.Type).IsModified = true;
                update.Property(x => x.Name).IsModified = true;
                update.Property(x => x.ShortDesc).IsModified = true;
                update.Property(x => x.Description).IsModified = true;
                update.Property(x => x.SortNumber).IsModified = true;
                


                if (Service.Image != "NoImage.jpg")
                {
                  
                    update.Property(x => x.Image).IsModified = true;
                
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        public Service GetServiceByID(int id)
        {
            return db.Services.Find(id);
        }
        public List<Service> GetAllServices()
        {
            return db.Services.OrderByDescending(a => a.ID).ToList();
        }

        public List<Category> GetListOfCategories()
        {
            var item = db.Categories.ToList();
            return item;
        }
        public List<int> GetChildCategories(int CategroyId)
        {
            List<int> subcategoryList = new List<int>();
            List<Category> category = null;
            int tempcategory = CategroyId;
            subcategoryList.Add(tempcategory);
            category = db.Categories.Where(m => m.ParentCategoryID == tempcategory).ToList();
            if (category != null)
            {
                foreach (var item in category)
                {
                    subcategoryList.Add(item.ID);
                }
            }
            return subcategoryList;
        }

        public bool isParentCategory(int CategoryId)
        {
            bool check = false;
            List<Category> category = db.Categories.Where(m => m.ParentCategoryID == CategoryId).ToList();
            if (category != null && category.Count() != 0)
            {
                check = true;
            }
            return check;
        }
        public bool Delete(int ColorID)
        {
            try
            {
                var item = db.Services.Where(x => x.ID == ColorID).FirstOrDefault();
                db.Services.Remove(item);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        //public List<ServiceSearch> ServiceSearch(int Row, string SKU, string Model, string Name, int CategoryID, int BrandID)
        //{
        //    string query = "select * from (select p.ID,p.Model,p.ServiceName,cat.CategoryName,b.BrandName,p.Price,isnull(p.Quantity,0) as Quantity, ROW_NUMBER() over(order by p.id desc) as RowNumber from ServiceCategories c join Service p on p.id=c.Serviceid join Categories cat on cat.ID= c.Categoryid join ServiceBrands b on b.ID = p.BrandID where 1=1";
        //    if (!string.IsNullOrEmpty(SKU))
        //    {
        //        query += "and p.SKU= '" + SKU + "'";
        //    }
        //    if (!string.IsNullOrEmpty(Model))
        //    {
        //        query += "and p.Model = '" + Model + "'";
        //    }
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        query += "and p.Servicename like '%" + Name + "%'";
        //    }
        //    if (CategoryID != 0)
        //    {
        //        query += "and c.categoryid =" + CategoryID;
        //    }
        //    if (BrandID != 0)
        //    {
        //        query += "and p.BrandID=" + BrandID;
        //    }
        //    int startRow = ((Row - 1) * 50) + 1;
        //    int EndRow = Row * 50;
        //    query += ") a where a.RowNumber>=" + startRow + " and a.RowNumber<=" + EndRow;
        //    List<ServiceSearch> prod = db.Database.SqlQuery<ServiceSearch>(query).ToList();
        //    List<ServiceSearch> p = new List<ServiceSearch>();
        //    return prod;
        //}
    }
}