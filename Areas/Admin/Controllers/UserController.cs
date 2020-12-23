using BookShop.Models.DAO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(string search, int page = 1, int pagesize = 5)
        {
            var model = new UserDAO().listUserPage(search, page, pagesize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(string name, string email, string password, string repassword, string phone, string adress)
        {
            var user = new User();
            var dao = new UserDAO();
            user.Name = name;
            if (!dao.Check_Exits_Email(email))
            {
                user.Email = email;
            }
            else
            {
                SetAlert("Email không tồn tại!! Xin kiểm tra lại...", "error");
                return View();
            }

            if (password == repassword)
            {
                user.Password = password;
            }
            else
            {
                SetAlert("Mật khẩu không khớp!! Xin kiểm tra lại...", "error");
                return View();
            }

            user.Phone = phone;
            user.Adress = adress;

            if (ModelState.IsValid)
            {
                var res = dao.AddUser(user);
                if (res)
                {
                    SetAlert("Thêm người dùng thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm người dùng không thành công", "error");
                    return View("Index");
                }
            }
            return View("Index");

        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(User entity)
        {
            if (ModelState.IsValid)
            {
                var res = new UserDAO().EditUser(entity);
                if (res)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật không thành công", "error");
                }
            }
            return View("Index");
        }


        [HttpPost]
        public JsonResult Delete(long id)
        {
            var res = new UserDAO().DeleteUser(id);
            if (res)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }
        }

        //đổi trạng thái status khi click vào 'Kích hoạt' or 'Khóa'
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
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