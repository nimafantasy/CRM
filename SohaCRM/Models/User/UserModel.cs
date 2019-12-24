using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohaCRM.Models.User
{
    public class UserModel
    {

        public int User_ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PersonelID { get; set; }
        public string Section { get; set; }
        public int Section_ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IsActive { get; set; }
        public int IsActive_ID { get; set; }
        public string Message { get; set; }
        public string MessageColor { get; set; }

    }
}