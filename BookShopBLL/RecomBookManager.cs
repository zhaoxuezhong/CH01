using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;

namespace BookShop.BLL
{
    public class RecomBookManager
    {
        public int PermitQuantity = 5;//允许最大推荐数量

        public bool AddRecomBook(int bookId, int userId)
        {
            return new RecomBookService().AddRecomBook(bookId, userId);
        }


        public bool RemoveRecomBook(int bookId)
        {
            return new RecomBookService().RemoveRecomBook(bookId);
        }


        public bool ExistBook(int bookId)
        {
            return new RecomBookService().ExistBook(bookId);
        }

        public int GetQuantity()
        {
            return new RecomBookService().GetQuantity();
        }

        public List<Book> GetBooks()
        {
            return new RecomBookService().GetBooks();
        }
    }
}
