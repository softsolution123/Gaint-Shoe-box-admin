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
    
    public partial class tblBanner
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Banner { get; set; }
        public string Url { get; set; }
        public string AddSone { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Delete { get; set; }
        public System.DateTime Created_Date { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<System.DateTime> Ending_Date { get; set; }
        public Nullable<byte> BannerRank { get; set; }
        public Nullable<byte> BannerType { get; set; }
        public Nullable<int> Hits { get; set; }
    }
}
