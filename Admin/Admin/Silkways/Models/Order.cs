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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderCarts = new HashSet<OrderCart>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<byte> DliveryStatusId { get; set; }
        public Nullable<byte> PaymentModeId { get; set; }
        public Nullable<byte> EmirateId { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Quantity { get; set; }
        public Nullable<int> SizeID { get; set; }
        public string AdditionalComments { get; set; }
        public string IpAddress { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> PromoCodeID { get; set; }
        public Nullable<decimal> TotalPriceAfterPromoCode { get; set; }
    
        public virtual DliveryStatu DliveryStatu { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual Order Order2 { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public virtual ProductSize ProductSize { get; set; }
        public virtual tblPromoCode tblPromoCode { get; set; }
        public virtual tblMember tblMember { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderCart> OrderCarts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
