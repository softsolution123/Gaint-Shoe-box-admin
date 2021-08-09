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
    using System.Collections.Generic;
    
    public partial class OrderShippingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderShippingDetail()
        {
            this.ShippingProductDetails = new HashSet<ShippingProductDetail>();
        }
    
        public long ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContactNo { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCityCode { get; set; }
        public string CustomerCity { get; set; }
        public string OrderShippingPrice { get; set; }
        public string OrderPaymentType { get; set; }
        public string ShippingOriginCity { get; set; }
        public string TotalOrderAmount { get; set; }
        public string OrderReferenceCode { get; set; }
        public string BlueExOrderCode { get; set; }
        public string CNCode { get; set; }
        public string Status { get; set; }
        public System.DateTime ShippingDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingProductDetail> ShippingProductDetails { get; set; }
    }
}