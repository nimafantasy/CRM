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
    
    public partial class Tbl_Customer_Products
    {
        public int ID { get; set; }
        public int Customer_ID { get; set; }
        public int Products_ID { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateTime { get; set; }
    
        public virtual Tbl_Customer Tbl_Customer { get; set; }
        public virtual Tbl_Products Tbl_Products { get; set; }
    }
}