using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models
{
    public class PreInvoice
    {
        int PreInvoice_ID { get; set; }
        int PreInvoiceType_ID { get; set; }
        string PreInvoiceNumber { get; set; }
        string IssueDate { get; set; }
        int SendingStatus { get; set; }
        byte[] PreInvoiceUpload { get; set; }
        int Description { get; set; }
        string LastUpdateUser_ID { get; set; }
    }
}