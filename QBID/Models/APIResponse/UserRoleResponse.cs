using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.Models.APIResponse
{
  
   
    public class UserRoleResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<UserRole> data { get; set; }
       
    }
    public class UserRole
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
