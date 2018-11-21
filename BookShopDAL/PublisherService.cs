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
    public class PublisherService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;

        public  Publisher GetPublisherById(int id)
        {
            string sql = "SELECT * FROM Publishers WHERE Id = @Id";
            Publisher publisher = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    publisher = new Publisher();
                    publisher.Id = (int)reader["Id"];
                    publisher.Name = (string)reader["Name"];
                    return publisher;
                }
            }
            return publisher;
        }

        public  List<Publisher> GetPublishers()
        {
            string sqlAll = "SELECT * FROM Publishers";
            return GetPublishersBySql(sqlAll);
        }


        private  List<Publisher> GetPublishersBySql(string safeSql)
        {
            List<Publisher> list = new List<Publisher>();
           
            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Publisher publisher = new Publisher();

                    publisher.Id = (int)row["Id"];
                    publisher.Name = (string)row["Name"];

                    list.Add(publisher);
                }
            }

            return list;
        }

    }
}
