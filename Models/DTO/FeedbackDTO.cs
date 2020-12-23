using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DTO
{
    public class FeedbackDTO
    {
        public long ID { get; set; }

        public long User_ID { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string Rating_Description { get; set; }

        public long Book_ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? Status { get; set; }

        public string User_Name { get; set; }
    }
}