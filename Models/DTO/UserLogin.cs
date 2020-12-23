using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class UserLogin
    {
        public string email { set; get; }
        public string pass { set; get; }
        public string confirmPass { set; get; }
        public string name { set; get; }
        public string phone { set; get; }
        public string adress { set; get; }
        public DateTime? birthday { set; get; }
    }
}