using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.DAL;
using BookShop.Models;
namespace BookShop.BLL
{
    public class BookManager
    {

        public bool ModifyBook(Book book)
        {
            return new BookService().ModifyBook(book);
        }

        public List<Book> GetBooks()
        {
            return new BookService().GetBooks();
        }

        public List<Book> GetNewBooks(int count)
        {
            return new BookService().GetNewBooks(count);
        }

        public List<Book> GetRankings(int count)
        {
            return new BookService().GetRankings(count);
        }

        public Book GetBookById(int id)
        {
            return new BookService().GetBookById(id);
        }

        public List<Book> GetBooks(BookQueryCategories category, string keyWord)
        {
            return new BookService().GetBooks(category, keyWord);
        }

        public bool DeleteBookById(int id)
        {
            return new BookService().DeleteBookById(id);
        }

        public void AddBook(Book book)
        {
            new  BookService().AddBook(book);
        }

        public List<Book> GetBooks(int categoryId, int pageSize, int currPageIndex, ref int pageCount, string sortField)
        {
            return new BookService().GetBooks(categoryId, pageSize, currPageIndex, ref  pageCount, sortField);
        }

        public void AddClick(int id)
        {
            new BookService().AddClick(id);
        }
    }
}
