using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.Accounting
{
    public class PreInvoiceListModel
    {
        public int PreInvoice_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public int PreInvoiceType_ID { get; set; }
        public IEnumerable<SelectListItem> PreInvoiceType { get; set; }
        public string PreInvoiceNumber { get; set; }
        public string IssueDate { get; set; }
        public int SendingStatus { get; set; }
        public int ConfirmationStatus { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public bool Check_Sent { get; set; }
        public bool Check_NotSent { get; set; }
        public bool Check_Confirmed { get; set; }
        public bool Check_NotConfirmed { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string CustomerNameInSearch { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string SubscriptionCode { get; set; }
        public int PaymentAmount { get; set; }
    }
}