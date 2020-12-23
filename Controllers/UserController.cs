using BookShop.Models.DAO;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OrderHistory(long ID)
        {
            ViewBag.OrderHistory = new UserDAO().OrderHistory(ID);
            return View();
        }

        public ActionResult Orders(long ID)
        {
            ViewBag.Orders = new UserDAO().Orders(ID);
            return View();
        }

        public ActionResult OrderDetail(long ID)
        {
            ViewBag.Order = new UserDAO().FindOrder(ID);
            ViewBag.OrderDetail = new UserDAO().OrderDetail(ID);
            return View();
        }

        public JsonResult CancerOrder(long Order_ID)
        {
            var res = new UserDAO().CancerOrder(Order_ID);
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

        //xuất bill
        public ActionResult Export(long ID)
        {
            ViewBag.UserOrder = new UserDAO().GetUser(ID);
            ViewBag.OrderDetail = new UserDAO().OrderDetail(ID);
            return View();
        }

        //In hóa đơn
        public ActionResult PrintBill(long ID)
        {
            var report = new ActionAsPdf("Export", new { ID = ID });
            return report;
        }
    }
}