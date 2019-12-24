using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.CustomerField
{
    public class CustomerFieldListModel
    {
        public int CustomerField_ID { get; set; }
        public string CustomerField_Name { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }
    }
}