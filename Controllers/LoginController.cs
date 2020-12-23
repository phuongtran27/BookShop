using BookShop.Models.DAO;
using BookShop.Models.DTO;
using BookShop.Models.EF;
using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.Controllers
{
    public class LoginController : Controller
    {
        public const string userSession = "userSession";

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login log)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var login = new UserDAO().CheckLogin(log.email, log.pass);
                if (login == -1)
                {
                    setAlert("Email không tồn tại", "error");
                }
                else if (login == 0)
                {
                    setAlert("Mật khẩu không đúng", "error");
                }
                else
                {
                    var user = dao.Find_Email(log.email);
                    var userSess = new User();
                    userSess.Email = user.Email;
                    userSess.Password = user.Password;
                    userSess.Name = user.Name;
                    Session.Add(userSession, user);//gán session để hiện tên người dùng khi đăng nhập thành công                   
                    return Redirect("/");
                }
            }
            return View();


        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.login = Session[userSession];
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserLogin user)
        {
            var Log = new UserDAO();
            if (ModelState.IsValid)
            {
                if (Log.CheckEmail(user.email))
                {
                    setAlert("Email đã tồn tại", "error");
                }
                //else if (!Log.Check_Exits_Email(user.email))
                //{
                //    setAlert("Email không hợp lệ hoặc chưa được đăng ký", "error");
                //}
                else
                {
                    var USER = new User();
                    var feedback = new Feedback();
                    USER.Email = user.email;
                    USER.Password = user.pass;
                    USER.Name = user.name;
                    USER.Phone = user.phone;
                    USER.Adress = user.adress;
                    if (user.birthday != null)
                    {
                        USER.DayOfBirth = user.birthday;
                    }

                    Session.Add(userSession, USER);//gán session để hiện tên người dùng khi đăng ký thành công                   


                    var res = new UserDAO().Insert(USER);
                    if (res)
                    {
                        setAlert("Đăng ký thành công", "success");
                        return Redirect("/");
                    }
                    else
                    {
                        setAlert("Đăng ký không thành công", "error");
                        return Redirect("/Dang-ky");
                    }

                }
            }
            return Redirect("/Dang-ky");
        }

        public ActionResult LogOut()
        {
            Session[userSession] = null;
            return Redirect("/");
        }

        public void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        //Đăng nhập bằng facebook
        [AllowAnonymous]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "330363828104718",
                client_secret = "c1cd4d98e20a6692a61621f9859704b7",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "330363828104718",
                client_secret = "c1cd4d98e20a6692a61621f9859704b7",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                //Lấy thông tin facebook
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstName = me.first_name;
                string middleName = me.middle_name;
                string lastName = me.last_name;

                var user = new User();
                user.Email = email;
                user.Status = true;
                user.Name = firstName + " " + middleName + " " + lastName;
                user.CreatedDate = DateTime.Now;
                var resultInsert = new UserDAO().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    var userSess = new User();
                    userSess.Email = user.Email;
                    userSess.Password = user.Password;
                    userSess.Name = user.Name;
                    Session.Add(userSession, userSess);//gán session để hiện tên người dùng khi đăng nhập thành công                   
                }

            }
            return Redirect("/");

        }
    }
}