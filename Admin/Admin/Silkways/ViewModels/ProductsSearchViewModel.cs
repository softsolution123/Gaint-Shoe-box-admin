using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Silkways.Models;

namespace Silkways.ViewModels
{
    public class ProductsSearchViewModel
    {
        
        public string SKU = "";
        public string Model = "";
        public string Name = "";
        public int CategoryID = 0;
        public int BrandID = 0;
        public int Row = 1;

        public ProductsSearchViewModel(int Row,string SKU, string Model, string Name, int CategoryID, int BrandID)
        {
            this.SKU = SKU;
            this.Model = Model;
            this.Name = Name;
            this.CategoryID = CategoryID;
            this.BrandID = BrandID;
            this.Row = Row;
        }
        public string pagination { get; set; }
        List<ProductSearch> product { get; set; }
        public List<ProductSearch> Products {
            get 
            {
                if (product != null)
                {
                    return product;
                }
                else
                {
                    //int pageNo = 0;
                    //if (HttpContext.Current.Request.QueryString["pageno"] != null)
                    //{
                    //    Row = Convert.ToInt32(HttpContext.Current.Request.QueryString["pageno"]);
                    //    pageNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["pageno"]) - 1;
                    //}

                    product = new ProductModel().ProductSearch(Row,SKU, Model, Name, CategoryID, BrandID);
                    //List<int> counted = new ProductModel().ProductSearchCount(Row, SKU, Model, Name, CategoryID, BrandID);
                    //int count = product.Count;
                    //pagination = Silkways.Models.SilkwaysFunction.GernalFunction.BuildBootstrapPagination(counted[0], "Product/Search", pageNo, 50);
                    return product;
                }
            }
        }
        public Product Product { get; set; }
    }
    public class ProductSearch
    {
        
        public int ID { get; set; }
        public string Model { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetPrice { get; set; }
        public string USPrice { get; set; }
        public int Quantity { get; set; }
        public Int64 RowNumber { get; set; }

        public string Authorname { get; set; }
        public string SKU { get; set; }
        public string isbn_number { get; set; }
        public string Url { get; set; }
        public decimal? Discountfixed { get; set; }
        //public string AutherName { get; set; }
    }
}