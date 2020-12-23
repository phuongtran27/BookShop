using BookShop.Models.DAO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookShop.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(long id)
        {
            var book = new BookDAO().ViewDetail(id);
            ViewBag.Book = new BookDAO().ViewDetail(id);
            new BookDAO().UpdateView(book.ID);
            var cate = new BookCategoryDAO().ViewDetail(book.CategoryID.Value);
            ViewBag.CategoryDetail = new CategoryDAO().ViewDetail(cate.ParentID.Value);
            ViewBag.BookDetail = cate;

            //Lấy bình luận của sách
            ViewBag.Feedback = new FeedBackDAO().getReview(book.ID);

            ViewBag.AsAuthor = new BookDAO().ListAsAuthor(book.Author);//Sách cùng tác giả
            ViewBag.AsCate = new BookDAO().ListAsCategory(book.CategoryID);    //Sách cùng thể loại
            return View();
        }

        public PartialViewResult CategoryBook()
        {
            ViewBag.Category = new CategoryDAO().ListAll();
            ViewBag.BookCategory = new BookCategoryDAO().ListAll();
            return PartialView();
        }



        public ActionResult FutureBookDetail(long id)
        {
            var book = new BookDAO().ViewDetail(id);
            ViewBag.Book = new BookDAO().ViewDetail(id);
            new BookDAO().UpdateView(book.ID);
            var cate = new BookCategoryDAO().ViewDetail(book.CategoryID.Value);
            ViewBag.CategoryDetail = new CategoryDAO().ViewDetail(cate.ParentID.Value);
            ViewBag.BookDetail = cate;
            return View();
        }

        //Sách cùng tác giả
        public ActionResult Author(string au)
        {
            var book = new BookDAO();
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            ViewBag.Author = book.Author(au);//Danh sách các sách của tác giả
            ViewBag.NameAu = au;//LẤy tên tác giả           
            return View();
        }

        //Lấy ra tất cả tác giả
        public ActionResult AllAuthor()
        {
            var book = new BookDAO();
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            ViewBag.allAuthor = book.AllAuthor();//lấy ra tất cả các tác giả

            var Author = book.AllAuthor();
            var listAu = new List<Book>();
            var list = new List<Book>();
            foreach (var item in Author)
            {
                listAu = book.Author(item);//Lấy ra sách có cùng nhà phát hành
                foreach (var itemRe in listAu)
                {
                    list.Add(itemRe);
                }
            }
            ViewBag.CountAuthor = list;//Lấy ra số lượng sách cho
            return View();
        }

        //Sách cùng nhà phát hành
        public ActionResult Release(string re)
        {
            var book = new BookDAO();
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            ViewBag.Release = book.Release(re);
            ViewBag.NameRe = re;
            return View();
        }

        //Tất cả nhà phát hành
        public ActionResult AllRelease()
        {
            var book = new BookDAO();
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu            
            ViewBag.allRelease = book.allRelease();
            var all = book.allRelease();
            var listRelease = new List<Book>();
            var list = new List<Book>();
            foreach (var item in all)
            {
                listRelease = book.Release(item);//Lấy ra sách có cùng nhà phát hành
                foreach (var itemRe in listRelease)
                {
                    list.Add(itemRe);
                }
            }
            ViewBag.CountBook = list;//Lấy ra số lượng sách cho
            return View();
        }

        //Hàm tìm kiếm sách
        public JsonResult ListName(string q)
        {
            var data = new BookDAO().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword)
        {
            BookShopDbContext db = new BookShopDbContext();
            var book = new BookDAO();
            ViewBag.BookViewLeft = book.ListViewMax(3);//lấy ra 3 thằng đầu
            string[] key = keyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            List<Book> author = new List<Book>();//Tìm theo tác giả
            List<Book> Book_Name = new List<Book>();//Tìm theo tên sách
            List<Book> Release = new List<Book>();//Tìm theo nhà phát hành
            foreach (var item in key)
            {
                author = (from b in db.Books
                          where b.Author.Contains(item)
                          select b).ToList();

                Book_Name = (from b in db.Books
                             where b.Name.Contains(item)
                             select b).ToList();

                Release = (from b in db.Books
                           where b.Released.Contains(item)
                           select b).ToList();
            }
            ViewBag.KeyWord = keyword;
            ViewBag.Author = author;
            ViewBag.Name = Book_Name;
            ViewBag.Release = Release;
            return View();
        }

        public JsonResult addReview(string json_review)
        {
            var JsonReview = new JavaScriptSerializer().Deserialize<List<Feedback>>(json_review);
            var fb = new Feedback();
            foreach (var item in JsonReview)
            {
                fb.Book_ID = item.Book_ID;
                fb.Content = item.Content;
                fb.Rating = item.Rating;
                fb.Rating_Description = item.Rating_Description;
                fb.User_ID = item.User_ID;
                fb.CreatedDate = DateTime.Now;
                fb.Status = true;
            }

            new FeedBackDAO().Insert(fb);
            return Json(new
            {
                status = true
            });
        }


    }
}