using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class CategoryDAO
    {
        BookShopDbContext db = null;

        public CategoryDAO()
        {
            db = new BookShopDbContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }

        public List<Category> FindID(long id)
        {
            return db.Categories.Where(x => x.ID == id).ToList();
        }

        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }

        //Lấy ra ảnh các danh mục ở cuối trang web
        public List<Category> ViewImage()
        {
            return db.Categories.Where(x => x.SeoTitle != null).ToList();
        }

    }
}