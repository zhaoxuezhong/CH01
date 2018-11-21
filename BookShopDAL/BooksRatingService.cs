using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BookShop.Models;
using System.Configuration;

namespace BookShop.DAL
{
    public class BooksRatingService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        public bool AddBookRating(BookRatings bookrating)
        {
            string sql =
                "INSERT BookRatings (BookId,UserId,Rating,Comment)" +
                "VALUES (@BookId, @UserId, @Rating, @Comment)";

            sql += " ; SELECT @@IDENTITY";


            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@BookId", bookrating.BookId), 
					new SqlParameter("@UserId", bookrating.User.Id), 
					new SqlParameter("@Rating", bookrating.Rating),
					new SqlParameter("@Comment",bookrating.Comment)
				};

            bookrating.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));
            return bookrating.Id > 0;
        }

        /// <summary>
        /// 根据书的Id得到其评价信息
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public List<BookRatings> GetBookRatings(int bookId)
        {
            string sql = "select * from bookratings where bookid='" + bookId + "'";
            return this.GetBookRatings(sql);
        }

        private List<BookRatings> GetBookRatings(string safeSql)
        {
            List<BookRatings> list = new List<BookRatings>();

           DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
           if (ds.Tables.Count > 0)
           {
               DataTable dt = ds.Tables[0];

               foreach (DataRow row in dt.Rows)
               {
                   BookRatings brating = new BookRatings();
                   brating.Id = (int)row["Id"];
                   brating.BookId = (int)row["BookId"];
                   brating.Rating = (int)row["Rating"];
                   int userId = (int)row["userid"];
                   brating.User = new UserService().GetUserById(userId);
                   brating.Comment = (string)row["Comment"];
                   brating.CreatedTime = (DateTime)row["CreatedTime"];
                   list.Add(brating);
               }
           }

            return list;

        }


    }
}
