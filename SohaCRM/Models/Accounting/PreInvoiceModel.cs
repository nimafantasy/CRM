using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.Accounting
{
    public class PreInvoiceModel
    {
        public int PreInvoice_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public int PreInvoiceType_ID { get; set; }
        public string PreInvoiceType { get; set; }
        public string PreInvoiceNumber { get; set; }
        public string IssueDate { get; set; }
        public int SendingStatus { get; set; }
        public int? ConfirmationStatus { get; set; }
        public string Status { get; set; }
        public string ConfStatus { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public bool Check_Sent { get; set; }
        public bool Check_NotSent { get; set; }
        public bool Check_Confirmed { get; set; }
        public bool Check_NotConfirmed { get; set; }
        public string File { get; set; }
        
            public string CustomerNameInSearch { get; set; }
        public string ReqDesc { get; set; }
        public string ReqType { get; set; }
        public string Amount { get; set; }
    }
}