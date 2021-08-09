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
    
    public partial class tblMember
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMember()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int memberId { get; set; }
        public string memberName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> memberTypeId { get; set; }
        public Nullable<int> registerType { get; set; }
        public string facebookID { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public string photo { get; set; }
        public Nullable<bool> Is_Active { get; set; }
        public Nullable<bool> Is_Delete { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public byte[] TimeStamp { get; set; }
        public string phoneNo { get; set; }
        public Nullable<bool> gender { get; set; }
        public string Img { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<byte> EmirateId { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string RecoverPassword { get; set; }
    
        public virtual tblCity tblCity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
