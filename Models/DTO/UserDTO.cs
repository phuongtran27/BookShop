using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }

        public long Order_ID { get; set; }
    }
}