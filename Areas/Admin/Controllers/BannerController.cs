using BookShop.Models.DAO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var model = new SlideDAO().listSlidePage(page, pagesize);

            ViewBag.Slide = new BookDAO().ListAll();
            return View(model);
        }

        // GET: Admin/Banner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Banner/Create
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        // POST: Admin/Banner/Create
        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var res = new SlideDAO().AddSlide(slide);
                if (res)
                {
                    SetAlert("Thêm mới thành công", "success");
                    SetViewBag();
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Thêm mới không thành công", "error");
                    return View();
                }
            }
            return View("Index");
        }

        // GET: Admin/Banner/Edit/5
        public ActionResult Edit(int id)
        {
            var slide = new SlideDAO().FindID(id);
            SetViewBag(slide.BookID);
            return View(slide);
        }

        // POST: Admin/Banner/Edit/5
        [HttpPost]
        public ActionResult Edit(Slide entity)
        {
            if (ModelState.IsValid)
            {
                var res = new SlideDAO().EditSlide(entity);
                if (res)
                {
                    SetAlert("Cập nhật slide thành công", "success");
                    return RedirectToAction("Index", "Banner");
                }
                else
                {
                    SetAlert("Cập nhật không thành công", "error");
                    return View();
                }
            }
            return View("Index");
        }



        // POST: Admin/Banner/Delete/5
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var res = new SlideDAO().DeleteSlide(id);
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
            var result = new SlideDAO().ChangeStatus(id);
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

        //dropdown list cho BookID
        public void SetViewBag(long? BookID = null)
        {
            var dao = new BookDAO();
            ViewBag.BookID = new SelectList(dao.ListAll(), "ID", "Name", BookID);
        }
    }
}