using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models
{
    public class GetDevice
    {
        int GetDevice_ID { get; set; }
        int Customer_ID { get; set; }
        string CustomerConnector { get; set; }
        string DeviceSerialNumber { get; set; }
        string RepairsInfo { get; set; }
        string TestDevice { get; set; }
        string ReturnDate { get; set; }
        string Description { get; set; }
        int LastUpdateUser_ID { get; set; }
    }
}