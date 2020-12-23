using BookShop.Models.DAO;
using BookShop.Models.DTO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Home
        public ActionResult Index()
        {
            var bookDao = new BookDAO();
            ViewBag.NewBook = bookDao.ListNewBook(7);//lấy ra những sản phẩm mới phát hành
            ViewBag.BookFuture = bookDao.ListBookFuture(4);//Lấy ra sản phẩm sắp phát hành
            ViewBag.BookView = bookDao.ListViewMax(4);//Lấy ra sản phẩm bán chạy
            ViewBag.BookViewLeft = bookDao.ListViewMax(3);//lấy ra 3 thằng đầu
            ViewBag.ViewCategory = new CategoryDAO().ViewImage();

            //Phần danh mục



            //hiển thị giỏ        
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var model = new MenuDAO().ListByGroupID(1);// 1 là typeID trong menu chính
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            var model = new FooterDAO().GetFooter();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult LeftMenu()
        {
            ViewBag.Category = new CategoryDAO().ListAll();
            ViewBag.BookCategory = new BookCategoryDAO().ListAll();
            return PartialView();
        }

        public PartialViewResult Slide()
        {
            ViewBag.slide = new SlideDAO().ListAll();
            ViewBag.Book = new BookDAO().ListAll();
            return PartialView();
        }

        //Lấy ra sách theo loại sách
        public ActionResult Category(long cateId)
        {
            var book = new BookDAO();
            ViewBag.BookCategory = book.ListCategoryBook(cateId);
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            ViewBag.BookDetail = new BookCategoryDAO().ViewDetail(cateId);
            return View();
        }

        //Lấy ra sách mới phát hành
        public ActionResult NewBook()
        {
            var book = new BookDAO();
            ViewBag.NewBook = book.ListNewBook();//lấy ra những sản phẩm mới phát hành
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            return View();
        }

        //Lấy ra sách sắp phát hành
        public ActionResult FutureBook()
        {
            var book = new BookDAO();
            ViewBag.FutureBook = book.ListBookFuture();
            ViewBag.BookViewLeft = book.ListViewMax(3);
            return View();
        }

        public ActionResult BookBuys()
        {
            var book = new BookDAO();
            ViewBag.BuysBook = book.ListTopBuys();
            ViewBag.BookViewLeft = book.ListViewMax(3);
            return View();
        }

        //phần danh mục
        public ActionResult ViewCategory(long id)
        {
            var Cate = new CategoryDAO().ViewDetail(id);
            ViewBag.Cate = Cate;
            var CateBook = new BookCategoryDAO().ListBookCategory(id);
            var book = new List<Book>();
            var Bok = new List<Book>();
            var BoDAO = new BookDAO();
            foreach (var item in CateBook)
            {
                if (BoDAO.CateGory(item.ID).Count > 0)
                {
                    book = BoDAO.CateGory(item.ID);
                    foreach (var itemBook in book)
                    {
                        Bok.Add(itemBook);
                    }
                }
            }
            ViewBag.BookViewLeft = BoDAO.ListViewMax(3);
            ViewBag.Book = Bok;
            return View();
        }

        //CHi tiết slide
        public ActionResult SlideDetail()
        {
            return View();
        }
    }
}