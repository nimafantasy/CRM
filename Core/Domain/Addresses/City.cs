﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Addresses
{
    public class City : BaseEntity
    {
        public string Name_Fa { get; set; }
        public string Name_En { get; set; }
        public string TelephoneCode { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public virtual Country Country { get; set; }
    }
}
