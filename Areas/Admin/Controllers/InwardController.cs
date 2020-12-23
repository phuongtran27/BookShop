using BookShop.Models.DAO;
using BookShop.Models.DTO;
using BookShop.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class InwardController : Controller
    {
        private BookShopDbContext db = new BookShopDbContext();
        // GET: Admin/Inward
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var model = db.Inwards.OrderByDescending(x => x.Createdate).ToPagedList(page, pageSize);
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddInward(Inward entity)
        {
            var res = new InwardDAO().addInward(entity);
            if (res)
            {
                var cart = (List<CartItem>)Session["add_inward"];
                foreach (var item in cart)
                {
                    var detail = new Inward_Detail();
                    detail.Inward_ID = db.Inwards.Max(x => x.ID);
                    detail.Book_ID = item.Book.ID;
                    detail.Quantity = item.Quantity;

                    detail.Price = item.Book.Price;
                    detail.Amount = (int)item.Book.Price * item.Quantity;
                    new InwardDAO().addInward_Detail(detail);

                    //Cộng tồn kho
                    new BookDAO().AddQuantity(item.Book.ID, item.Quantity);
                }
                Session["add_inward"] = null;
                TempData["add_success"] = "Nhập kho thành công.";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }
        }


        public JsonResult addInwardBook(string book_name, int quantity)
        {
            var book = new BookDAO().searchBook(book_name);
            var cart = Session["add_inward"];
            if (cart != null)//Nếu giỏ đã chứa sản phẩm
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Book.ID == book.ID))
                {
                    foreach (var item in list)
                    {
                        if (item.Book.ID == book.ID)
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
                    list.Add(item);
                }
            }
            else//nếu giỏ hàng trống
            {
                var item = new CartItem();
                item.Book = book;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                Session["add_inward"] = list;
            }
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);

        }


        //Xóa từng sản phẩm
        public JsonResult Delete_InwardBook(long ID)
        {
            var cartSec = (List<CartItem>)Session["add_inward"];
            cartSec.RemoveAll(x => x.Book.ID == ID);
            Session["add_inward"] = cartSec;
            return Json(new
            {
                status = true
            });
        }

        //Sửa số lượng sp trong giỏ hàng
        public JsonResult Edit(long ID, int Quantity)
        {
            var bookSec = (List<CartItem>)Session["add_inward"];

            foreach (var item in bookSec)
            {
                if (item.Book.ID == ID)
                {
                    item.Quantity = Quantity;
                }

            }

            Session["add_inward"] = bookSec;
            return Json(new
            {
                status = true
            });
        }


        //Chi tiết nhập kho
        public ActionResult Inward_Detail(long ID, int page = 1, int pageSize = 10)
        {
            var model = from detail in db.Inward_Detail
                        join book in db.Books on detail.Book_ID equals book.ID
                        where detail.Inward_ID == ID
                        select new InwardDTO()
                        {
                            Inward_Detail_ID = detail.ID,
                            Book_Image = book.Image,
                            Book_Name = book.Name,
                            Quantity = detail.Quantity,
                            Price = detail.Price,
                            Amount = detail.Amount
                        };
            ViewBag.Inward = db.Inwards.Find(ID);
            return View(model.OrderByDescending(x => x.Quantity).ToPagedList(page, pageSize));
        }
    }
}