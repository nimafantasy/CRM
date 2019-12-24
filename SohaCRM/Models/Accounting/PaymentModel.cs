using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.Accounting
{
    public class PaymentModel
    {
        public int Payment_ID { get; set; }
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public string PreInvoiceNumber { get; set; }
        public string PaymentDate { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string SubscriptionCode { get; set; }
        public int PaymentAmount { get; set; }
        public string CustomerNameInSearch { get; set; }
        public string InvoiceNumber { get; set; }
        public string ReqType { get; set; }
        public string Amount { get; set; }
        public string SendingStatus { get; set; }
        public string ConfirmationStatus { get; set; }
    }
}