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
    
    public partial class PanelMenuUserRelation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int PanelMenuID { get; set; }
        public bool IsActive { get; set; }
    
        public virtual PanelMenu PanelMenu { get; set; }
        public virtual User User { get; set; }
    }
}