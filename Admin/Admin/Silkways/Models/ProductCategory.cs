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
    
    public partial class ProductCategory
    {
        public int ID { get; set; }
        public short CategoryID { get; set; }
        public int ProductID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
