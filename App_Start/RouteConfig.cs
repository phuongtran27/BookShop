using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Rewrite URL Đăng Xuất
            routes.MapRoute(
                name: "User logout",
                url: "Dang-xuat",
                defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL Đăng Ký
            routes.MapRoute(
                name: "User Register",
                url: "Dang-ky",
                defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );


            //Rewrite URL Đăng nhập bằng facebook
            routes.MapRoute(
                name: "User Login facebook",
                url: "facebook-login",
                defaults: new { controller = "Login", action = "LoginFacebook", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );


            //Rewrite URL Đăng nhập
            routes.MapRoute(
                name: "User Login",
                url: "Dang-nhap",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL Thêm giỏ hàng
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL lấy ra sách theo danh mục
            routes.MapRoute(
                name: "Book Category",
                url: "the-loai/{metatitle}-{cateId}",
                defaults: new { controller = "Home", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL lấy ra danh mục sách ở cuối trang
            routes.MapRoute(
                name: "Category Book",
                url: "danh-muc/{metatitle}-{id}",
                defaults: new { controller = "Home", action = "ViewCategory", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL lấy ra sách mới
            routes.MapRoute(
                name: "New Book",
                url: "sach-moi",
                defaults: new { controller = "Home", action = "NewBook", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL lấy ra sách sắp phát hành
            routes.MapRoute(
                name: "Future Book",
                url: "sap-phat-hanh",
                defaults: new { controller = "Home", action = "FutureBook", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL lấy ra sách sắp phát hành
            routes.MapRoute(
                name: "Buys Book",
                url: "sach-ban-chay",
                defaults: new { controller = "Home", action = "BookBuys", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL Thanh toán
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL Thanh toán thành công
            routes.MapRoute(
                name: "Payment Success",
                url: "thanh-toan-thanh-cong",
                defaults: new { controller = "Cart", action = "Payment_success", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL cập nhật giỏ hàng
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );



            //Rewrite URL Chi tiết sản phẩm
            routes.MapRoute(
                name: "Book Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Book", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL Chi tiết sản phẩm
            routes.MapRoute(
                name: " Future Book Detail",
                url: "sap-phat-hanh/{metatitle}-{id}",
                defaults: new { controller = "Book", action = "FutureBookDetail", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL sách cùng tác giả
            routes.MapRoute(
                name: "As Author",
                url: "Tac-gia/{au}",
                defaults: new { controller = "Book", action = "Author", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL sách cùng tác giả
            routes.MapRoute(
                name: "All Author",
                url: "Tat-ca-tac-gia",
                defaults: new { controller = "Book", action = "AllAuthor", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL sách cùng nhà phát hành
            routes.MapRoute(
                name: "Release",
                url: "Nha-phat-hanh/{re}",
                defaults: new { controller = "Book", action = "Release", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );


            //Rewrite URL sách cùng nhà phát hành
            routes.MapRoute(
                name: "All Release",
                url: "Nha-phat-hanh",
                defaults: new { controller = "Book", action = "AllRelease", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );

            //Rewrite URL sách cùng nhà phát hành
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Book", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BookShop.Controllers" }
            );
        }
    }
}
