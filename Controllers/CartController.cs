using BookShop.Models.DAO;
using BookShop.Models.DTO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            //hiển thị giỏ        
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        //Thêm mới sản phẩm vào giỏ
        public ActionResult AddItem(long BookID, int quantity)
        {
            var book = new BookDAO().ViewDetail(BookID);
            var cart = Session[CartSession];
            if (cart != null)//Nếu giỏ đã chứa sản phẩm
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Book.ID == BookID))//nếu giỏ đã chứa sản phẩm có ID = BookID
                {
                    foreach (var item in list)
                    {
                        if (item.Book.ID == BookID && quantity <= book.Quantity)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //Tạo đối tượng mới
                    var item = new CartItem();
                    item.Book = book;
                    item.Quantity = quantity;
                    item.actual_number = (int)book.Quantity;
                    list.Add(item);
                }
            }
            else//nếu giỏ hàng trống
            {
                var item = new CartItem();
                item.Book = book;
                item.Quantity = quantity;
                item.actual_number = (int)book.Quantity;
                var list = new List<CartItem>();
                list.Add(item);

                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }


        public JsonResult Edit(string cartModel)
        {
            var JsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var cartSec = (List<CartItem>)Session[CartSession];

            foreach (var item in cartSec)
            {
                var jsonItem = JsonCart.SingleOrDefault(x => x.Book.ID == item.Book.ID);
                if (jsonItem != null && item.actual_number >= jsonItem.Quantity)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[CartSession] = cartSec;
            return Json(new
            {
                status = true
            });
        }

        //Xóa toàn bộ giỏ hàng
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        //Xóa từng sản phẩm
        public JsonResult Delete(long id)
        {
            var cartSec = (List<CartItem>)Session[CartSession];
            cartSec.RemoveAll(x => x.Book.ID == id);
            Session[CartSession] = cartSec;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(Order entity)
        {
            var order = new Order();
            order.User_ID = entity.User_ID;
            order.CreateDate = DateTime.Now;
            order.ShipName = entity.ShipName;
            order.ShipMobile = entity.ShipMobile;
            order.ShipAdress = entity.ShipAdress;
            order.ShipEmail = entity.ShipEmail;
            order.TotalMoney = entity.TotalMoney;
            order.TotalQuantity = entity.TotalQuantity;
            order.Status = 1;

            try
            {
                var id = new OrderDAO().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];

                var detailDao = new Order_DetailDAO();
                foreach (var item in cart)
                {
                    var orderdetail = new Order_Detail();
                    orderdetail.BookID = item.Book.ID;
                    orderdetail.Quantity = item.Quantity;
                    orderdetail.OrderID = id;
                    orderdetail.Price = item.Book.Price;
                    detailDao.Insert(orderdetail);

                }
            }
            catch (Exception e)
            {
                return Redirect("/loi-thanh-toan");
            }

            Session[CartSession] = null;
            return Redirect("/thanh-toan-thanh-cong");
        }

        //Cập nhật  lại lượt mua sách khi khách hàng thanh toán
        public JsonResult Success(string id)
        {
            var IDPay = new JavaScriptSerializer().Deserialize<List<CartItem>>(id);
            var cartSec = (List<CartItem>)Session[CartSession];
            foreach (var item in cartSec)
            {
                var json = IDPay.SingleOrDefault(x => x.Book.ID == item.Book.ID);
                item.Book.Buys++;
                json.Book.Buys = item.Book.Buys;
                new BookDAO().UpdateBuys(json.Book.ID);
            }
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Payment_success()
        {
            return View();
        }


    }
}