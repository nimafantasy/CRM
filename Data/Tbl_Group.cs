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
    
    public partial class Tbl_Group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Group()
        {
            this.Tbl_Group_Activity = new HashSet<Tbl_Group_Activity>();
            this.Tbl_User_Group = new HashSet<Tbl_User_Group>();
        }
    
        public int Group_ID { get; set; }
        public string GroupName { get; set; }
        public int Active { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Group_Activity> Tbl_Group_Activity { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_User_Group> Tbl_User_Group { get; set; }
    }
}