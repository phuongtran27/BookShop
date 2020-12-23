using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class InwardDAO
    {
        BookShopDbContext db = null;
        public InwardDAO()
        {
            db = new BookShopDbContext();
        }

        public bool addInward(Inward entity)
        {
            try
            {
                var inward = new Inward();
                inward.TotalAmount = entity.TotalAmount;
                inward.TotalQuantity = entity.TotalQuantity;
                inward.Createdate = DateTime.Now;

                db.Inwards.Add(inward);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void addInward_Detail(Inward_Detail entity)
        {
            db.Inward_Detail.Add(entity);
            db.SaveChanges();
        }
    }
}