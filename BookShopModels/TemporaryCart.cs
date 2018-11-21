using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Models
{
    public class TemporaryCart
    {
        private int id;
        private Book book;
        private User user;
        private DateTime createTime;


        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }
       

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        public Book Book
        {
            get { return book; }
            set { book = value; }
        }
       

     
      
    }
}
