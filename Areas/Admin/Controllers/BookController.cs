using BookShop.Models.DAO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        // GET: Admin/Book
        public ActionResult Index(string search, int page = 1, int pagesize = 5)
        {
            var model = new BookDAO().listBookPage(search, page, pagesize);
            ViewBag.Search = search;
            return View(model);
        }

        // GET: Admin/Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Book/Create
        public ActionResult Create()
        {
            ViewBag.Form = new SelectList(new BookDAO().Form());
            SetViewBag();
            return View();
        }

        // POST: Admin/Book/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var res = new BookDAO().addBook(book);
                if (res)
                {
                    SetAlert("Thêm mới thành công", "success");
                    SetViewBag();
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm không thành công", "error");
                    return View();
                }
            }
            return View("Index");

        }

        // GET: Admin/Book/Edit/5
        public ActionResult Edit(long id)
        {
            var book = new BookDAO().ViewDetail(id);
            SetViewBag(book.CategoryID);
            ViewBag.Form = new SelectList(new BookDAO().Form());
            return View(book);
        }



        // POST: Admin/Book/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Book book)
        {
            ViewBag.Form = new SelectList(new BookDAO().Form());
            SetViewBag();
            var res = new BookDAO().EditBook(book);
            if (res)
            {
                SetAlert("Cập nhật sách thành công", "success");
                return RedirectToAction("Index", "Book");
            }
            else
            {
                SetAlert("Cập nhật không thành công", "error");
                return View();
            }


        }


        // POST: Admin/Book/Delete/5
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var res = new BookDAO().DeleteBook(id);
            if (res)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }
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

        //Lấy ra thông tin dropdownList
        public void SetViewBag(long? selectID = null)
        {
            var dao = new BookCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectID);
        }
    }
}