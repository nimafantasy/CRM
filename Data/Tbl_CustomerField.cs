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
    
    public partial class Tbl_CustomerField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_CustomerField()
        {
            this.Tbl_Customer = new HashSet<Tbl_Customer>();
        }
    
        public int CustomerField_ID { get; set; }
        public string CustomerField_Name { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Customer> Tbl_Customer { get; set; }
    }
}