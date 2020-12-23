using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class AdminDAO
    {
        BookShopDbContext db = null;
        public AdminDAO()
        {
            db = new BookShopDbContext();
        }

        public bool LoginAdmin(string user, string pass)
        {
            var ad = db.Admins.SingleOrDefault(x => x.UserName == user && x.PassWord == pass);
            if (ad == null)
            {
                return false;
            }
            return true;

        }
    }
}