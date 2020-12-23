using BookShop.Models.DTO;
using BookShop.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BookShop.Models.DAO
{
    public class UserDAO
    {
        BookShopDbContext db = null;

        public UserDAO()
        {
            db = new BookShopDbContext();
        }

        public User ViewDetail(long id)
        {
            return db.Users.Find(id);
        }

        public bool Insert(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }


        public int CheckLogin(string email, string pass)
        {
            if (db.Users.Count(x => x.Email == email) <= 0)
            {
                return -1;
            }
            else if (db.Users.Count(x => x.Password == pass) <= 0)
            {
                return 0;
            }

            return 1;
        }

        public User Find_Email(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        public bool Check_Exits_Email(string email)
        {
            using (WebClient webclient = new WebClient())
            {
                string url = "http://verify-email.org/";
                NameValueCollection formdata = new NameValueCollection();
                formdata["check"] = email;
                byte[] responsebyte = webclient.UploadValues(url, "POST", formdata);
                string reponse = Encoding.ASCII.GetString(responsebyte);
                if (reponse.Contains("Result: Ok"))
                    return true;
                return false;
            }
        }

        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == entity.Email);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }
        }

        //Phân trang trong admin
        public IEnumerable<User> listUserPage(string searchString, int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }

        //Sửa trạng thái người dùng
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);//tìm ra id
            user.Status = !user.Status;//khi click vào status nó sẽ đổi thành "kích hoạt" or "khóa" tùy thuộc vào trạng thái ban đầu
            db.SaveChanges();
            return (bool)user.Status;
        }

        //Sửa thông tin người dùng
        public bool EditUser(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Adress = entity.Adress;
                user.Status = entity.Status;
                user.Password = entity.Password;
                user.CreatedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteUser(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Thêm người dùng
        public bool AddUser(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }


        //lịch sử mua hàng
        public List<Order> OrderHistory(long UserID)
        {
            return db.Orders.Where(x => x.Status == 0 || x.Status == 3).OrderByDescending(x => x.CreateDate).ToList();
        }

        //chi tiết đơn hàng
        public List<Order_DetailDTO> OrderDetail(long OrderID)
        {
            var query = from detail in db.Order_Detail
                        join order in db.Orders on detail.OrderID equals order.ID
                        join book in db.Books on detail.BookID equals book.ID
                        where detail.OrderID == OrderID
                        select new Order_DetailDTO()
                        {
                            Book_Name = book.Name,
                            Image = book.Image,
                            OrderID = order.ID,
                            Price = book.Price,
                            Quantity = detail.Quantity
                        };

            return query.OrderByDescending(x => x.Book_Name).ToList();
        }

        public Order FindOrder(long OrderID)
        {
            return db.Orders.Find(OrderID);
        }

        //Đơn hàng của bạn
        public List<Order> Orders(long UserID)
        {
            return db.Orders.Where(x => x.Status == 1 || x.Status == 2).OrderByDescending(x => x.CreateDate).ToList();
        }

        //hủy đơn hàng
        public bool CancerOrder(long Order_ID)
        {
            try
            {
                var order = db.Orders.Find(Order_ID);
                order.Status = 0;
                order.CancerDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Lấy họ tên chủ đơn hàng
        public UserDTO GetUser(long Order_ID)
        {
            var query = from order in db.Orders
                        join user in db.Users on order.User_ID equals user.ID
                        select new UserDTO()
                        {
                            Name = user.Name,
                            Phone = user.Phone,
                            Adress = user.Adress,
                            Email = user.Email,
                            Order_ID = order.ID
                        };
            return query.FirstOrDefault(x => x.Order_ID == Order_ID);
        }
    }
}