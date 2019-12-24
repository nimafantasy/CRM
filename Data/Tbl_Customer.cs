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
    
    public partial class Tbl_Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Customer()
        {
            this.Tbl_ChangeSoftware = new HashSet<Tbl_ChangeSoftware>();
            this.Tbl_Customer_Products = new HashSet<Tbl_Customer_Products>();
            this.Tbl_DeviceFailure = new HashSet<Tbl_DeviceFailure>();
            this.Tbl_GetDevice = new HashSet<Tbl_GetDevice>();
            this.Tbl_Hardware = new HashSet<Tbl_Hardware>();
            this.Tbl_Installation = new HashSet<Tbl_Installation>();
            this.Tbl_OtherRequest = new HashSet<Tbl_OtherRequest>();
            this.Tbl_Payment = new HashSet<Tbl_Payment>();
            this.Tbl_PreInvoice = new HashSet<Tbl_PreInvoice>();
            this.Tbl_ReturnRepairedDevice = new HashSet<Tbl_ReturnRepairedDevice>();
            this.Tbl_SendToRepair = new HashSet<Tbl_SendToRepair>();
            this.Tbl_Software = new HashSet<Tbl_Software>();
            this.Tbl_SupportServices = new HashSet<Tbl_SupportServices>();
            this.Tbl_Training = new HashSet<Tbl_Training>();
        }
    
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerConnector { get; set; }
        public string SubscriptionCode { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string Email { get; set; }
        public string CompanyRegistrationName { get; set; }
        public string EconomicalNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string NationalID { get; set; }
        public int CustomerField_ID { get; set; }
        public int Active { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ChangeSoftware> Tbl_ChangeSoftware { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Customer_Products> Tbl_Customer_Products { get; set; }
        public virtual Tbl_CustomerField Tbl_CustomerField { get; set; }
        public virtual Tbl_User Tbl_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DeviceFailure> Tbl_DeviceFailure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_GetDevice> Tbl_GetDevice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Hardware> Tbl_Hardware { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Installation> Tbl_Installation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OtherRequest> Tbl_OtherRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Payment> Tbl_Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_PreInvoice> Tbl_PreInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ReturnRepairedDevice> Tbl_ReturnRepairedDevice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SendToRepair> Tbl_SendToRepair { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Software> Tbl_Software { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_SupportServices> Tbl_SupportServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Training> Tbl_Training { get; set; }
    }
}
