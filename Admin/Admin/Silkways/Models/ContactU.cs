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
    
    public partial class ContactU
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Pro_time { get; set; }
        public string Pro_budget { get; set; }
        public string Message { get; set; }
        public string Company_name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Status { get; set; }
        public string File_name { get; set; }
        public string SourceURL { get; set; }
        public Nullable<int> SourceID { get; set; }
        public Nullable<int> InquiryStatusID { get; set; }
        public string Note { get; set; }
    
        public virtual InquiryStatu InquiryStatu { get; set; }
    }
}