using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class CartItem
    {
        public Book Book { set; get; }
        public int Quantity { set; get; }
        public int actual_number { get; set; }
    }
}