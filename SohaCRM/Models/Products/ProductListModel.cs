using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SohaCRM.Models.Products
{
    public class ProductListModel
    {
        public int Product_ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
        //public string IsActive { get; set; }
        //public int IsActive_ID { get; set; }
        //public IEnumerable<SelectListItem> Active { get; set; }
    }
}