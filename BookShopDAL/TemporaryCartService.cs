using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using BookShop.Models;


namespace BookShop.DAL
{

    public class TemporaryCartService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;

        public bool AddItem(int bookId, int userId)
        {
            string sql =
               "INSERT TemporaryCart (BookId, UserId)" +
                "VALUES (@BookId, @UserId)";
            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@BookId", bookId), //FK
                    new SqlParameter("@UserId", userId) //FK
                };
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;

        }

        public bool RemoveItem(int id)
        {
            string sql = "DELETE FROM TemporaryCart WHERE Id=" + id;
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql) > 0;

        }

        public List<TemporaryCart> GetItems(int userId)
        {
            List<TemporaryCart> list = new List<TemporaryCart>();
            string sql = "SELECT * FROM TemporaryCart WHERE UserId=" + userId;
            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    TemporaryCart item = new TemporaryCart();
                    item.Id = Convert.ToInt32(row["Id"]);
                    item.Book = new BookService().GetBookById(Convert.ToInt32(row["BookId"]));
                    item.User = new UserService().GetUserById(Convert.ToInt32(row["UserId"]));
                    item.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                    list.Add(item);
                }
            }
            return list;
        }

        public bool ExistItem(int userId, int bookId)
        {
            string sql = "SELECT COUNT(0) FROM TemporaryCart WHERE BookId={0} AND UserId={1}";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, string.Format(sql, bookId, userId))) != 0;
        }

    }
}
