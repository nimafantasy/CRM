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
    
    public partial class Tbl_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_User()
        {
            this.Tbl_ChangeSoftware = new HashSet<Tbl_ChangeSoftware>();
            this.Tbl_Customer = new HashSet<Tbl_Customer>();
            this.Tbl_DeviceFailure = new HashSet<Tbl_DeviceFailure>();
            this.Tbl_GetDevice = new HashSet<Tbl_GetDevice>();
            this.Tbl_Group = new HashSet<Tbl_Group>();
            this.Tbl_Group_Activity = new HashSet<Tbl_Group_Activity>();
            this.Tbl_Hardware = new HashSet<Tbl_Hardware>();
            this.Tbl_HardwareType = new HashSet<Tbl_HardwareType>();
            this.Tbl_Installation = new HashSet<Tbl_Installation>();
            this.Tbl_OtherRequest = new HashSet<Tbl_OtherRequest>();
            this.Tbl_Payment = new HashSet<Tbl_Payment>();
            this.Tbl_PreInvoice = new HashSet<Tbl_PreInvoice>();
            this.Tbl_Products = new HashSet<Tbl_Products>();
            this.Tbl_RequestType = new HashSet<Tbl_RequestType>();
            this.Tbl_ReturnRepairedDevice = new HashSet<Tbl_ReturnRepairedDevice>();
            this.Tbl_SendToRepair = new HashSet<Tbl_SendToRepair>();
            this.Tbl_Software = new HashSet<Tbl_Software>();
            this.Tbl_SoftwareType = new HashSet<Tbl_SoftwareType>();
            this.Tbl_SupportServices = new HashSet<Tbl_SupportServices>();
            this.Tbl_Training = new HashSet<Tbl_Training>();
            this.Tbl_User_Group = new HashSet<Tbl_User_Group>();
            this.Tbl_User_Group1 = new HashSet<Tbl_User_Group>();
        }
    
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonnelID { get; set; }
        public int Section_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ChangeSoftware> Tbl_ChangeSoftware { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Customer> Tbl_Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DeviceFailure> Tbl_DeviceFailure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_GetDevice> Tbl_GetDevice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Group> Tbl_Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Group_Activity> Tbl_Group_Activity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Hardware> Tbl_Hardware { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_HardwareType> Tbl_HardwareType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Installation> Tbl_Installation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OtherRequest> Tbl_OtherRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Payment> Tbl_Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_PreInvoice> Tbl_PreInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Products> Tbl_Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_RequestType> Tbl_RequestType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ReturnRepairedDevice> Tbl_ReturnRepairedDevice { get; set; }
        public virtual Tbl_Section Tbl_Section { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SendToRepair> Tbl_SendToRepair { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Software> Tbl_Software { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SoftwareType> Tbl_SoftwareType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SupportServices> Tbl_SupportServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Training> Tbl_Training { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_User_Group> Tbl_User_Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_User_Group> Tbl_User_Group1 { get; set; }
    }
}
