//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Silkways.Models
{
    using System;
    
    public partial class GetNewProductsByHighprice_Result
    {
        public short CategoryID { get; set; }
        public int ProductID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<short> SortOrder { get; set; }
        public Nullable<short> ParentCategoryID { get; set; }
        public string CategoryImage { get; set; }
        public string CatIcon { get; set; }
        public int ProdID { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public string SKU { get; set; }
        public Nullable<decimal> Price { get; set; }
        public decimal Discount { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCompany { get; set; }
        public string ProductFeatures { get; set; }
        public string ProductSpecification { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ShortDescription { get; set; }
        public string ProductTags { get; set; }
        public int PhotoID { get; set; }
        public int ProdPhotoId { get; set; }
        public string LargePhoto { get; set; }
        public string MedPhoto { get; set; }
        public string Thumbnail { get; set; }
        public Nullable<bool> IsExclusive { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> DiscountFixed { get; set; }
        public Nullable<decimal> NetPrice { get; set; }
        public string Url { get; set; }
    }
}