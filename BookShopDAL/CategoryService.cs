
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BookShop.Models;
using System.Configuration;


namespace BookShop.DAL
{
    public class CategoryService
    {
        
        public List<Category> GetCategories()
        {
            string strSql = "SELECT * FROM Categories ORDER BY SortNum ASC";
            return GetCategories(strSql);
        }

        public void AddCategory(Category category)
        {
            string sql =
                "INSERT Categories (Name)" +
                "VALUES (@Name)";

            sql += " ; SELECT @@IDENTITY";


            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Name", category.Name)
				};

            category.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionString, CommandType.Text, sql, para));
        }

        public Category GetCategoryById(Int32 id)
        {
            string sql = "SELECT * FROM Categories WHERE Id = @Id";
            Category category = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionString, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    category = new Category();

                    category.Id = (int)reader["Id"];
                    category.Name = (string)reader["Name"];
                    category.PId = (int)reader["PId"];
                }
            }
            return category;
        }

        private List<Category> GetCategories(string sql, params SqlParameter[] values)
        {
            List<Category> list = new List<Category>();
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.Text, sql, values);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Category category = new Category();

                    category.Id = (int)row["Id"];
                    category.Name = (string)row["Name"];
                    category.PId = (int)row["PId"];
                    list.Add(category);
                }
            }
            return list;

        }

        private List<Category> GetCategories(string safeSql)
        {
            List<Category> list = new List<Category>();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Category category = new Category();

                    category.Id = (int)row["Id"];
                    category.Name = (string)row["Name"];
                    category.PId = (int)row["PId"];
                    list.Add(category);
                }
            }
            return list;

        }

    }
}
