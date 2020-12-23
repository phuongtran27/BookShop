using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class InwardDTO
    {
        public long Inward_Detail_ID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Amount { get; set; }

        public string Book_Name { get; set; }
        public string Book_Image { get; set; }
    }
}