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
    
    public partial class OrderCart
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        public int OrderID { get; set; }
    
        public virtual tblcart tblcart { get; set; }
        public virtual Order Order { get; set; }
    }
}
