using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

using BookShop.Models;
namespace BookShop.DAL
{
    public class RecomBookService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        public bool AddRecomBook(int bookId, int userId)
        {
            string sql =
                          "INSERT RecomBooks (BookId, UserId)" +
                          "VALUES (@BookId, @UserId)";


            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@BookId", bookId), 
                    new SqlParameter("@UserId", userId) 
                   
                };
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;
        }


        public bool RemoveRecomBook(int bookId)
        {
            string sql = "DELETE FROM RecomBooks WHERE BookId=" + bookId;
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql) > 0;
        }


        public bool ExistBook(int bookId)
        {
            string sql = "SELECT COUNT(0) FROM RecomBooks WHERE BookId=" + bookId;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql)) != 0;
        }

        public int GetQuantity()
        {
            string sql = "SELECT COUNT(0) FROM RecomBooks";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql));
        }

        public List<Book> GetBooks()
        {
            string sqlAll = "SELECT Books.* FROM Books INNER JOIN RecomBooks ON RecomBooks.BookId=Books.Id";
            return new BookService().GetBooksBySql(sqlAll);
        }
    }
}
