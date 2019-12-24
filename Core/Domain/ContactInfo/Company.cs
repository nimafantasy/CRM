using Core.Domain.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ContactInfo
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string TelNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string EconomicCode { get; set; }
        public string OfficialName { get; set; }
        public string RegistrationNumber { get; set; }
        public string NationalId { get; set; }
        public bool Active { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
