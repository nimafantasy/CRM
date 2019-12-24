using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.CustomerProducts
{
    public class CustomerProductsModel
    {
        public int Customer_ID { get; set; }
        public string CustomerName { get; set; }
        public int Products_ID { get; set; }
        public string ProductsName { get; set; }
    }
}