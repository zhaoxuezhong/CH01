using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Models
{
    public class BookRatings
    {
        private int id;
        private int bookid;
        private User user;
        private int rating;
        private string comment = String.Empty;
        private DateTime createdTime;

        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }

        public BookRatings() { }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int BookId
        {
            get { return this.bookid; }
            set { this.bookid = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }


        public int Rating
        {
            get { return this.rating; }
            set { this.rating = value; }
        }

        public string Comment
        {
            get { return this.comment; }

            set { this.comment = value; }
        }
    }
}
