using BookShop.Models.DTO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private BookShopDbContext db = new BookShopDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            //thống kê sách bán ra
            var sell = from detail in db.Order_Detail
                       join order in db.Orders on detail.OrderID equals order.ID
                       where order.Status == 3
                       select new
                       {
                           detail.Quantity,
                           detail.Price
                       };
            ViewBag.Count_sell = sell.Select(x => x.Quantity).Sum();

            //Thống kê doanh thu
            ViewBag.Count_money = sell.Select(x => x.Price * x.Quantity).Sum();

            //Thống kê đơn đặt hàng
            ViewBag.Count_Order = db.Orders.ToList().Count();

            //Thống kê tồn ko
            ViewBag.quantity_book = db.Books.Where(x => x.Quantity > 0).OrderByDescending(x => x.Quantity).Take(10).ToList();

            //Thống kê lượng hàng bán ra
            var lstbook_id = from detail in db.Order_Detail
                             join order in db.Orders on detail.OrderID equals order.ID
                             where order.Status == 1
                             select new
                             {
                                 detail.BookID
                             };
            var listbook_sell = new List<OrderDTO>();
            foreach (var item in lstbook_id.Select(x => x.BookID).Distinct())
            {
                var book = db.Books.Find(item);
                var booksell = new OrderDTO();
                booksell.Book_Name = book.Name;
                booksell.Quantity = 0;
                booksell.Price = 0;
                var query = from detail in db.Order_Detail
                            join order in db.Orders on detail.OrderID equals order.ID
                            where order.Status == 1 && detail.BookID == item
                            select new
                            {
                                detail.Quantity,
                                detail.Price
                            };
                foreach (var jtem in query)
                {
                    booksell.Quantity += jtem.Quantity;
                    booksell.Price += jtem.Price * jtem.Quantity;
                }
                listbook_sell.Add(booksell);
            }
            ViewBag.book_sell = listbook_sell.OrderByDescending(x => x.Quantity).Take(10).ToList();

            //Thống kê đánh giá hôm nay
            ViewBag.Review_today = db.Feedbacks.Where(x => DbFunctions.TruncateTime(x.CreatedDate) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            //Thống kê đơn đạt hàng hôm nay
            ViewBag.Order_today = db.Orders.Where(x => DbFunctions.TruncateTime(x.CreateDate) == DbFunctions.TruncateTime(DateTime.Now)).Count();

            //Thống kê user đã đăng ký
            ViewBag.user = db.Users.Count();

            //Thống kê nhập kho
            var input = (from detail in db.Inward_Detail
                         join inward in db.Inwards on detail.Inward_ID equals inward.ID
                         where DbFunctions.TruncateTime(inward.Createdate) == DbFunctions.TruncateTime(DateTime.Now)
                         select new
                         {
                             detail.Book_ID
                         });
            ViewBag.inward = input.Select(x => x.Book_ID).Distinct().Count();

            //Đơn hàng chờ thanh toán
            ViewBag.Order_wait = db.Orders.Where(x => x.Status == 0).Count();
            return View();
        }
    }
}