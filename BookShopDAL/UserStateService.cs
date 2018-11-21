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
    public class UserStateService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;

        public  UserState GetUserStateById(int id)
        {
            string sql = "SELECT * FROM UserStates WHERE Id = @Id";
            UserState userState = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    userState = new UserState();
                    userState.Id = (int)reader["Id"];
                    userState.Name = (string)reader["Name"];
                }
            }
            return userState;
        }


    }
}
