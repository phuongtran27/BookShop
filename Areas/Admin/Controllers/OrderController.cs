using BookShop.Models.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var model = new OrderDAO().getAll();
            return View(model.ToPagedList(page, pagesize));
        }

        public ActionResult Order_Detail(long ID)
        {
            var model = new OrderDAO().getOrder_detail(ID);
            ViewBag.Order = new OrderDAO().findID(ID);
            return View(model);
        }

        public JsonResult changeStatus(long ID)
        {
            new OrderDAO().changeStatus(ID);

            //Trừ tồn kho
            foreach(var item in new Order_DetailDAO().getOrder_Detail(ID))
            {
                new BookDAO().SubQuantity(item.Book.ID, (int)item.Quantity);
            }
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long ID)
        {
            var res = new OrderDAO().Delete(ID);
            if (res)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
            
        }

        public JsonResult Delete_OrderDetail(long ID)
        {
            var res = new OrderDAO().Delete_OrderDetail(ID);
            if (res)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}