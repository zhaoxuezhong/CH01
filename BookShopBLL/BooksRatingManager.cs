using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.DAL;
using BookShop.Models;
namespace BookShop.BLL
{
   public  class BooksRatingManager
    {

        public  List<BookRatings> GetBookRatings(int bookId)
        {
            return new BooksRatingService().GetBookRatings(bookId);
        }

        public  bool AddBookRating(BookRatings bookrating)
        {
            return new BooksRatingService().AddBookRating(bookrating);
        }
    }
}
