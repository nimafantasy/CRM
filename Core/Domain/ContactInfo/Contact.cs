using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ContactInfo
{
    public class Contact : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelePhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public Company Company { get; set; }
        public int TestScore { get; set; }
        public bool IsTrained { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
