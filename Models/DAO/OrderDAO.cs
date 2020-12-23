using BookShop.Models.DTO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class OrderDAO
    {
        BookShopDbContext db = null;
        public OrderDAO()
        {
            db = new BookShopDbContext();
        }

        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public Order findID(long ID)
        {
            return db.Orders.Find(ID);
        }

        public List<Order> getAll()
        {
            return db.Orders.OrderByDescending(x => x.CreateDate).ToList();
        }

        //chi tiết đơn hàng
        public List<OrderDTO> getOrder_detail(long order_id)
        {
            var query = from detail in db.Order_Detail
                        join order in db.Orders on detail.OrderID equals order.ID
                        join book in db.Books on detail.BookID equals book.ID
                        where detail.OrderID == order_id
                        select new OrderDTO()
                        {
                            OrderDetail_ID = detail.ID,
                            Book_Name = book.Name,
                            Quantity = detail.Quantity,
                            Price = detail.Price
                        };
            return query.ToList();
        }

        //Thanh toán đơn hàng
        public void changeStatus(long ID)
        {
            var order = db.Orders.Find(ID);
            if (order.Status == 1)
                order.Status = 2;
            else if (order.Status == 2)
                order.Status = 3;
            db.SaveChanges();
        }

        //Xóa đơn hàng
        public bool Delete(long ID)
        {
            try
            {
                var order = db.Orders.Find(ID);
                db.Orders.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        //Xóa chi tiết đơn hàng
        public bool Delete_OrderDetail(long ID)
        {
            try
            {
                var detail = db.Order_Detail.Find(ID);
                db.Order_Detail.Remove(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}