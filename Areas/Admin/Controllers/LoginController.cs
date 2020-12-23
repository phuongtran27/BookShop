using BookShop.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            var log = new AdminDAO().LoginAdmin(username, password);
            if (!log)
            {
                SetAlert("Tên tài khoản hoặc mật khẩu không đúng!!", "error");
                return View();
            }
            else
            {
                Session["user_admin"] = "Administrator";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["user_admin"] = null;
            return RedirectToAction("Index");
        }

        public void SetAlert(string message, string type)
        {
            //Giống ViewBag
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "Warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}