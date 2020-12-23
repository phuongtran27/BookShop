using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class Order_DetailDTO
    {
        public long BookID { get; set; }
        public long OrderID { get; set; }
        public Nullable<int> TotalQuantity { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Book_Name { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }

        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}