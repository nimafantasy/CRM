//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_SoftwareType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_SoftwareType()
        {
            this.Tbl_Software = new HashSet<Tbl_Software>();
        }
    
        public int Software_ID { get; set; }
        public string SoftwareName { get; set; }
        public int Products_ID { get; set; }
        public int SoftwarePrice { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        public virtual Tbl_Products Tbl_Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Software> Tbl_Software { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
    }
}