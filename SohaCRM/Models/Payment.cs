using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models
{
    public class Payment
    {
        int Payment_ID { get; set; }
        int Customer_ID { get; set; }
        string PreInvoiceNumber { get; set; }
        string PaymentDate { get; set; }
        int PaymentAmount { get; set; }
        string Description { get; set; }
        int LastUpdateUser_ID { get; set; }
    }
}