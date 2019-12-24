using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.SoftwareType
{
    public class SoftwareType
    {
        public int Software_ID { get; set; }
        public string SoftwareName { get; set; }
        public int Products_ID { get; set; }
        public int SoftwarePrice { get; set; }
        public string Description { get; set; }
        public int LastUpdateUser_ID { get; set; }
    }
}