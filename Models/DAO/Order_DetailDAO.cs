using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class Order_DetailDAO
    {
        BookShopDbContext db = null;
        public Order_DetailDAO()
        {
            db = new BookShopDbContext();
        }

        public bool Insert(Order_Detail detail)
        {
            try
            {
                db.Order_Detail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Order_Detail> getOrder_Detail(long ID)
        {
            return db.Order_Detail.Where(x => x.OrderID == ID).ToList();
        }
    }
}