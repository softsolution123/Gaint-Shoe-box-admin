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
    
    public partial class ContentUnit
    {
        public int ID { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int UserID { get; set; }
    
        public virtual User User { get; set; }
    }
}
