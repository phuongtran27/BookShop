using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class BookCategoryDAO
    {
        BookShopDbContext db = null;

        public BookCategoryDAO()
        {
            db = new BookShopDbContext();
        }

        public List<BookCategory> ListAll()
        {
            return db.BookCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public BookCategory ViewDetail(long id)
        {
            return db.BookCategories.Find(id);
        }

        public List<BookCategory> ListBookCategory(long id)
        {
            return db.BookCategories.Where(x => x.ParentID == id).ToList();
        }
    }
}