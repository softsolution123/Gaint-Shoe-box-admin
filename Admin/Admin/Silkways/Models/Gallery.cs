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
    
    public partial class Gallery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gallery()
        {
            this.GalleryPhotos = new HashSet<GalleryPhoto>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Url { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public Nullable<int> SortNumber { get; set; }
        public string Image { get; set; }
        public string VideoEmbed { get; set; }
        public bool IsHome { get; set; }
        public Nullable<int> CategoryID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GalleryPhoto> GalleryPhotos { get; set; }
    }
}
