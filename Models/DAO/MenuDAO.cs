using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class MenuDAO
    {
        BookShopDbContext db = null;
        public MenuDAO()
        {
            db = new BookShopDbContext();
        }

        //Lấy ra menu chính theo typeID
        public List<Menu> ListByGroupID(int groupID)
        {
            return db.Menus.Where(x => x.TypeID == groupID).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}