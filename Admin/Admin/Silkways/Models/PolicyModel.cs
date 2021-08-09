using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;
using System.Data.Entity;

namespace Silkways.Models
{
    public class PolicyModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public void Save(Policy policy)
        {
            try
            {
                db.Policies.Add(policy);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void Update(Policy policy)
        {
            try
            {
                db.Policies.Attach(policy);
                var update = db.Entry(policy);
                update.Property(x => x.PolicyCategory).IsModified = true;
                update.Property(x => x.Description).IsModified = true;
                update.Property(x => x.FileName).IsModified = true;
            

                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        public Policy GetPolicyByID(int id)
        {
            return db.Policies.Find(id);
        }
        public List<Policy> GetAllPolicies()
        {
            return db.Policies.Include(x=>x.PolicyCategory1).OrderByDescending(a => a.ID).ToList();
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
                var item = db.Policies.Where(x => x.ID == ColorID).FirstOrDefault();
                db.Policies.Remove(item);
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