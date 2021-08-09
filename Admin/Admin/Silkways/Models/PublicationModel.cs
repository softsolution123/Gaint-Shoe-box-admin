using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Models
{
    public class PublicationModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public Publication Save(Publication publication)
        {
            try
            {
                db.Publications.Add(publication);
                db.SaveChanges();
                return publication;
            }
            catch (Exception ex)
            {
                return publication;
            }
        }

        public List<Product> SearchPro(int Row)
        {
            string Query = "select  top(50) * from ( select  ROW_NUMBER() over(order by p.ID desc)as RowNumber, p.*, COUNT(*) OVER() AS TotalCount  from dbo.Product p   where p.IsDelete = 0 ";
            Query += "  ) a where a.RowNumber > " + Row + " * 50";
            return db.Products.SqlQuery(Query).ToList();
        }
        public int GetCountPro()
        {
            return db.Products.Where(x => x.IsDelete == false).Count();
        }

        public void Update(Publication publication)
        {
            try
            {
                db.Publications.Attach(publication);
                var update = db.Entry(publication);
                update.Property(x => x.Name).IsModified = true;
                update.Property(x => x.SortOrder).IsModified = true;
                update.Property(x => x.Type).IsModified = true;

                if (publication.PdfFile != null)
                {
                    update.Property(x => x.PdfFile).IsModified = true;
                }
                if (publication.Image != null)
                {
                    update.Property(x => x.Image).IsModified = true;
                }

              
            
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        //public void UpdateProductQuantityByID(int ProductID)
        //{
        //    Product prod = GetProductByID(ProductID);
        //    prod.Quantity = 0;
        //    db.SaveChanges();
        //}
        public Publication GetByID(int id)
        {
            return db.Publications.Find(id);
        }
        public Product GetProductByName(string ProductName)
        {
            return db.Products.FirstOrDefault(x => x.ProductName == ProductName);
        }

        public List<Publication> GetAllPublications()
        {
            return db.Publications.ToList();
        }
        public void Delete(int id)
        {
            var item = db.Publications.Where(x => x.ID == id).FirstOrDefault();
            if (item != null)
            {
                db.Publications.Remove(item);
                db.SaveChanges();
            }
            
        }
        public void Stockup(int id)
        {
            var item = db.Products.Where(x => x.ID == id).SingleOrDefault();
            if (item.IsStock == true)
            {
                item.IsStock = false;
            }
            else
            {
                item.IsStock = true;
            }
            db.Products.Attach(item);
            var update = db.Entry(item);
            update.Property(x => x.IsStock).IsModified = true;

            db.SaveChanges();
        }
        public void Deletephotos(int id)
        {
            var item = db.ProductPhotos.Where(x => x.PhotoID == id).SingleOrDefault();
            db.ProductPhotos.Remove(item);
            db.SaveChanges();
        }
        public void Deleteorderdetail(int id)
        {
            var item = db.OrderDetails.Where(x => x.ProductId == id);
            foreach (var ite in item)
            {
                db.OrderDetails.Remove(ite);    
            }
            
            db.SaveChanges();
        }

        public List<Category> GetListOfCategories()
        {
            var item = db.Categories.ToList();
            return item;
        }
        public List<ProductSearch> ProductSearch(int Row, string SKU, string Model, string Name, int CategoryID, int BrandID)
        {
            if (!string.IsNullOrEmpty(SKU) || !string.IsNullOrEmpty(Model) || !string.IsNullOrEmpty(Name) || CategoryID != 0 || BrandID != 0)
            {
                
                string query = "select p.Url,p.SKU,p.USPrice,p.isbn_number,p.AuthorName,p.ID,p.Model,p.ProductName,cat.CategoryName,b.BrandName,p.Price,p.DiscountPercentage,p.Discount,p.Discountfixed,p.NetPrice,isnull(p.Quantity,0) as Quantity, ROW_NUMBER() over(order by p.id desc) as RowNumber from productCategories c join product p on p.id=c.productid join Categories cat on cat.ID= c.Categoryid join ProductBrands b on b.ID = p.BrandID where 1=1";
                if (!string.IsNullOrEmpty(SKU))
                {
                    query += "and p.Authorname= '" + SKU + "'";
                }
                if (!string.IsNullOrEmpty(Model))
                {
                    query += "and p.isbn_number = '" + Model + "'";
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    query += "and p.productname like '%" + Name + "%'";
                }
                if (CategoryID != 0)
                {
                    query += "and c.categoryid =" + CategoryID + " or cat.ParentCategoryID = " + CategoryID ;
                }
                if (BrandID != 0)
                {
                    query += "and p.BrandID=" + BrandID;
                }
                //int startRow = ((Row - 1) * 50) + 1;
                //int EndRow = Row * 50;
                //query += ") a where a.RowNumber>="+startRow+" and a.RowNumber<="+EndRow;
                List<ProductSearch> prod = db.Database.SqlQuery<ProductSearch>(query).ToList();
                List<ProductSearch> p = new List<ProductSearch>();
                return prod;
            }
            else
            {
                List<ProductSearch> P = new List<ProductSearch>();
                return P;
            }
        }

        public List<int> GetChildCategories(int CategroyId)
        {
            List<int> subcategoryList = new List<int>();
            List<Category> category = null;
            int tempcategory = CategroyId;
            subcategoryList.Add(tempcategory);
            category = db.Categories.Where(m => m.ParentCategoryID == tempcategory).ToList();
            if(category != null)
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
            if(category != null && category.Count() != 0)
            {
                check = true;
            }
            return check;
        }

        public List<int> ProductSearchCount(int Row, string SKU, string Model, string Name, int CategoryID, int BrandID)
        {
            string query = "select count(*) from productCategories c join product p on p.id=c.productid join Categories cat on cat.ID= c.Categoryid join ProductBrands b on b.ID = p.BrandID where 1=1";
            if (!string.IsNullOrEmpty(SKU))
            {
                query += "and p.SKU= '" + SKU + "'";
            }
            if (!string.IsNullOrEmpty(Model))
            {
                query += "and p.Model = '" + Model + "'";
            }
            if (!string.IsNullOrEmpty(Name))
            {
                query += "and p.productname like '%" + Name + "%'";
            }
            if (CategoryID != 0)
            {
                query += "and c.categoryid =" + CategoryID;
            }
            if (BrandID != 0)
            {
                query += "and p.BrandID=" + BrandID;
            }
            //int startRow = ((Row - 1) * 50) + 1;
            //int EndRow = Row * 50;
            //query += ") a where a.RowNumber>=" + startRow + " and a.RowNumber<=" + EndRow;
            //var abc = db.Database.SqlQuery<int>(query).ToList();

            List<ProductSearch> p = new List<ProductSearch>();
            return db.Database.SqlQuery<int>(query).ToList();
        }
        public void UpdateProductByID(int ID, decimal DiscountPercentage, decimal DiscountFixed)
        {
            Product P = db.Products.FirstOrDefault(x => x.ID == ID);
            decimal netPrice = 0;
            if (P != null)
            {
                P.Discount = DiscountPercentage;
                P.Discountfixed = DiscountFixed;
                if (P.Discount != 0)
                {
                    netPrice = Convert.ToDecimal(P.Price) - ((Convert.ToDecimal(P.Discount) / 100) * Convert.ToDecimal(P.Price));
                    P.TotalPrice = Math.Round(netPrice, 2);
                }
                else
                {
                    P.TotalPrice = Convert.ToDecimal(P.Price) - P.Discountfixed;
                    P.TotalPrice = Math.Round(Convert.ToDecimal(P.TotalPrice), 2);
                    
                }
            }
            db.SaveChanges();

        }

        public void UpdateProductByID(int ID, decimal Price, int Quantity, decimal DiscountPercentage, decimal DiscountFixed, decimal NetTotal, int BrandID, string Url, string AutherName, string SKU, string isbn_number)
        {
            Product P = db.Products.FirstOrDefault(x => x.ID == ID);
            if (P != null)
            {
                P.Url = Url;
                //P.Authorname = AutherName;
                P.SKU = SKU;
                P.isbn_number = isbn_number;
                P.BrandID = BrandID;
                P.Price = Price;
                P.Quantity = Quantity;
                P.Discount = DiscountPercentage;
                P.Discountfixed = DiscountFixed;
                P.TotalPrice = NetTotal;
                //if (P.Discountfixed == 0 && P.DiscountPercentage == 0)
                //{
                //    P.NetPrice = Price;
                //}
                db.SaveChanges();
            }
        }


        //public List<ProductSearch> ProductSearch(int Row, string SKU, string Model, string Name, int CategoryID, int BrandID)
        //{
        //    string query = "select * from (select p.ID,p.Model,p.ProductName,cat.CategoryName,b.BrandName,p.Price,isnull(p.Quantity,0) as Quantity, ROW_NUMBER() over(order by p.id desc) as RowNumber from productCategories c join product p on p.id=c.productid join Categories cat on cat.ID= c.Categoryid join ProductBrands b on b.ID = p.BrandID where 1=1";
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
        //        query += "and p.productname like '%" + Name + "%'";
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
        //    List<ProductSearch> prod = db.Database.SqlQuery<ProductSearch>(query).ToList();
        //    List<ProductSearch> p = new List<ProductSearch>();
        //    return prod;
        //}
    }
}