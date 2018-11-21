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
    public class UserRoleService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        public  UserRole GetUserRoleById(int id)
        {
            UserRole userRole = null;
            string sql = "SELECT * FROM UserRoles WHERE Id = @Id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    userRole = new UserRole();
                    userRole.Id = (int)reader["Id"];
                    userRole.Name = (string)reader["Name"];
                }
            }
            return userRole;
        }


    }
}
