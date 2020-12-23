using BookShop.Models.DTO;
using BookShop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Models.DAO
{
    public class FeedBackDAO
    {
        BookShopDbContext db = null;

        public FeedBackDAO()
        {
            db = new BookShopDbContext();
        }

        public void Insert(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
        }

        public List<FeedbackDTO> getReview(long book_id)
        {
            var query = from fb in db.Feedbacks
                        join user in db.Users on fb.User_ID equals user.ID
                        where fb.Book_ID == book_id
                        select new FeedbackDTO()
                        {
                            ID = fb.ID,
                            User_Name = user.Name,
                            Content = fb.Content,
                            CreatedDate = fb.CreatedDate,
                            Rating = (int)fb.Rating,
                            Rating_Description = fb.Rating_Description
                        };
            return query.OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}