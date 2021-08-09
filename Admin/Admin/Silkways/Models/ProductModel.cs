using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Silkways.Models;
using Silkways.ViewModels;

namespace Silkways.Models
{
    public class ProductModel
    {
        GaintShoeBoxEntities db = new GaintShoeBoxEntities();
        public Product Save(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product;
            }
            catch (Exception ex)
            {
                return product;
            }
        }

        public List<Subscriber> SubscriberList()
        {
            return db.Subscribers.OrderByDescending(x => x.ID).ToList();

        }


        //public void SaveSizeID(tblProduct_Size Size)
        //{

        //    try
        //    {
        //        db.tblProduct_Size.Add(Size);
        //        db.SaveChanges();
        //        return;
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //}


        public void DeleteSizes(int id)
        {
            //var Sizes = db.tblProduct_Size.Where(x => x.ProductID == id).ToList();
            //if (Sizes.Count != 0)
            //{
            //    foreach (var obj in Sizes)
            //    {
            //        db.tblProduct_Size.Remove(obj);
            //    }

            //}
            //db.SaveChanges();
            //foreach (var Size in Sizes)
            //{

            //    var item = db.tblProduct_Size.Where(x=>x.);


            //}
            //if (item != null)
            //{
            //    db.Products.Remove(item);
            //    db.SaveChanges();
            //}

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

        public void Update(Product product, List<ProductPhoto> product_images, List<int> productSizes , List<int> tblColors)
        {
            try
            {
                if (product.ID > 0)
                {
                    Product Product = db.Products.Where(x => x.ID == product.ID).FirstOrDefault();
                    if (Product != null)
                    {


                        Product.ProductName = product.ProductName;
                        Product.Quantity = product.Quantity;
                        Product.ShortDescription = product.ShortDescription;
                        Product.Price = product.Price;
                        Product.Discount = product.Discount;
                        Product.Description = product.Description;
                        Product.VideoEmbed = product.VideoEmbed;
                        Product.ShowHome = product.ShowHome;





                        Product.SmallLength = product.SmallLength;
                        Product.SmallWidth = product.SmallWidth;
                        Product.SmallHeight = product.SmallHeight;
                        Product.SmallPairOfShoes = product.SmallPairOfShoes;
                        Product.SmallPrice = product.SmallPrice;



                        Product.MediumLength = product.MediumLength;
                        Product.MediumWidth = product.MediumWidth;
                        Product.MediumHeight = product.MediumHeight;
                        Product.MediumPairOfShoes = product.MediumPairOfShoes;
                        Product.MediumPrice = product.MediumPrice;


                        Product.LargeLength = product.LargeLength;
                        Product.LargeWidth = product.LargeWidth;
                        Product.LargeHeight = product.LargeHeight;
                        Product.LargePairOfShoes = product.LargePairOfShoes;
                        Product.LargePrice = product.LargePrice;






                        Product.BrandID = product.BrandID;

                        Product.SizeID = productSizes.FirstOrDefault();

                        Product.DiscountPrice = product.DiscountPrice;
                        Product.TotalPrice = product.TotalPrice;

                        Product.IsDelete = false;
                        //Product.Url= product.ProductName;
                        Product.Url = product.Url;

                        db.Entry<Product>(Product).State = EntityState.Modified;
                        db.SaveChanges();

                        if (tblColors != null && tblColors.Count() > 0)
                        {
                            var productColor = db.tblProductColors.Where(x => x.ProductID == Product.ID).ToList();
                            if (productColor != null && productColor.Count() > 0)
                            {
                                db.tblProductColors.RemoveRange(productColor);
                                db.SaveChanges();
                            }
                            foreach (var item in tblColors)
                            {
                                tblProductColor ProductColor = new tblProductColor();
                                ProductColor.ColorID = item;
                                ProductColor.ProductID = Product.ID;
                                db.tblProductColors.Add(ProductColor);
                                db.SaveChanges();
                            }
                        }

                        if (product_images != null && product_images.Count() > 0)
                        {
                            foreach (var item in product_images)
                            {
                                ProductPhoto ProductImage = new ProductPhoto();
                                ProductImage.Thumbnail = item.Thumbnail;
                                ProductImage.ProductID = Product.ID;
                                db.ProductPhotos.Add(ProductImage);
                                db.SaveChanges();
                            }
                        }


                        //var sizes = db.ProductSizes.Where(x => x.ProductID == Product.ID).ToList();


                        //if (sizes.Count != 0)
                        //{
                        //    foreach (var size in sizes)
                        //    {
                        //        db.ProductSizes.Remove(size);
                        //    }

                        //}


                        //if (productSizes != null && productSizes.Count() > 0)
                        //{
                        //    foreach (var item in productSizes)
                        //    {
                        //        ProductSize ProductSize = new ProductSize();
                        //        ProductSize.SizeID = item.SizeID;
                        //        ProductSize.ProductID = Product.ID;
                        //        ProductSize.Quantity = item.Quantity;

                        //        db.ProductSizes.Add(ProductSize);
                        //        db.SaveChanges();
                        //    }
                        //}






                    }
                }
                else
                {
                    Product Product = new Product();
                    Product.ProductName = product.ProductName;
                    Product.Quantity = product.Quantity;
                    Product.ShortDescription = product.ShortDescription;
                    Product.Price = product.Price;
                    Product.BrandID = product.BrandID;
                    Product.Discount = product.Discount;
                    Product.Description = product.Description;
                    Product.VideoEmbed = product.VideoEmbed;
                    Product.ShowHome = product.ShowHome;
                    Product.SizeID = productSizes.FirstOrDefault();
                    Product.IsDelete= false;
                    Product.Url = product.ProductName;
                    Product.DiscountPrice = product.DiscountPrice;
                    Product.TotalPrice = product.TotalPrice;

                    Product.SmallLength = product.SmallLength;
                    Product.SmallWidth = product.SmallWidth;
                    Product.SmallHeight = product.SmallHeight;
                    Product.SmallPairOfShoes = product.SmallPairOfShoes;
                    Product.SmallPrice = product.SmallPrice;



                    Product.MediumLength = product.MediumLength;
                    Product.MediumWidth = product.MediumWidth;
                    Product.MediumHeight = product.MediumHeight;
                    Product.MediumPairOfShoes = product.MediumPairOfShoes;
                    Product.MediumPrice = product.MediumPrice;


                    Product.LargeLength = product.LargeLength;
                    Product.LargeWidth = product.LargeWidth;
                    Product.LargeHeight = product.LargeHeight;
                    Product.LargePairOfShoes = product.LargePairOfShoes;
                    Product.LargePrice = product.LargePrice;


                    Product = db.Products.Add(Product);
                    db.SaveChanges();



                    if (tblColors != null && tblColors.Count() > 0)
                    {
                        var productColor = db.tblProductColors.Where(x => x.ProductID == Product.ID).ToList();
                        if (productColor != null && productColor.Count() > 0)
                        {
                            db.tblProductColors.RemoveRange(productColor);
                            db.SaveChanges();
                        }
                        foreach (var item in tblColors)
                        {
                            tblProductColor ProductColor = new tblProductColor();
                            ProductColor.ColorID = item;
                            ProductColor.ProductID = Product.ID;
                            db.tblProductColors.Add(ProductColor);
                            db.SaveChanges();
                        }
                    }

                    if (product_images != null && product_images.Count() > 0)
                    {
                        foreach (var item in product_images)
                        {
                            ProductPhoto ProductImage = new ProductPhoto();
                            ProductImage.Thumbnail = item.Thumbnail;
                            ProductImage.ProductID = Product.ID;
                            db.ProductPhotos.Add(ProductImage);
                            db.SaveChanges();
                        }
                    }

                    //if (productSizes != null && productSizes.Count() > 0)
                    //{
                    //    foreach (var item in productSizes)
                    //    {
                    //        ProductSize ProductSize = new ProductSize();
                    //        ProductSize.SizeID = item.SizeID;
                    //        ProductSize.ProductID = Product.ID;
                    //        ProductSize.Quantity = item.Quantity;

                    //        db.ProductSizes.Add(ProductSize);
                    //        db.SaveChanges();
                    //    }
                    //}

                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void UpdateProductQuantityByID(int ProductID)
        {
            Product prod = GetProductByID(ProductID);
            prod.Quantity = 0;
            db.SaveChanges();
        }
        public Product GetProductByID(int id)
        {
            return db.Products.Find(id);
        }


        public tblProductColor getColor(int id)
        {
            return db.tblProductColors.Where(x=>x.ProductID==id).FirstOrDefault();
        }



        public List<tblProductColor> getColors(int id)
        {
            return db.tblProductColors.Where(x => x.ProductID == id).ToList();
        }


        public Product GetProductByName(string ProductName)
        {
            return db.Products.FirstOrDefault(x => x.ProductName == ProductName);
        }

        public List<Product> GetAllProducts()
        {
            return db.Products.Where(x=>x.IsDelete==false).ToList();
        }


        public List<tblColor> GetallColor()
        {
            //return db.tblProductColors.ToList();
            return db.tblColors.ToList();
        }



        public List<ProductSize> GetallSizes()
        {
            //return db.tblProductColors.ToList();
            return db.ProductSizes.ToList();
        }



        public List<ProductBrand> GetBrands()
        {
            //return db.tblProductColors.ToList();
            return db.ProductBrands.ToList();
        }




        public void Delete(int id)
        {

            //var Sizes = db.tblProduct_Size.Where(x => x.ProductID == id).ToList();
            //if (Sizes.Count != 0)
            //{
            //    foreach (var obj in Sizes)
            //    {
            //        db.tblProduct_Size.Remove(obj);
            //    }

            //}
            //db.SaveChanges();

            var item = db.Products.Where(x => x.ID == id).FirstOrDefault();
            if (item != null)
            {
                db.Products.Remove(item);
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