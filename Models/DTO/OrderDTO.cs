using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class OrderDTO
    {
        public DateTime? CreateDate { get; set; }

        public long? CreatID { get; set; }

        public string ShipName { get; set; }

        public string ShipMobile { get; set; }

        public string ShipAdress { get; set; }

        public string ShipEmail { get; set; }

        public int? Status { get; set; }

        public long OrderDetail_ID { get; set; }

        public long OrderID { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public long BookID { get; set; }

        public string Book_Name { get; set; }
    }
}