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
    
    public partial class tblSlider
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<byte> SortOrder { get; set; }
        public string Title { get; set; }
        public string SubHeading { get; set; }
        public string SubHeadingTwo { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public bool IsExclusive { get; set; }
        public string Description { get; set; }
    }
}
