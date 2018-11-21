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
    public class UserService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        /// <summary>
        /// 根据登录名查询用户
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public  User GetUserByLoginId(string loginId)
        {
            string sql = "SELECT * FROM Users WHERE LoginId = @LoginId";
            int userStateId;
            int userRoleId;
            User user = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@LoginId", loginId)))
            {
                if (reader.Read())
                {
                    user = new User();

                    user.Id = (int)reader["Id"];
                    user.LoginId = (string)reader["LoginId"];
                    user.LoginPwd = (string)reader["LoginPwd"];
                    user.Name = (string)reader["Name"];
                    user.Address = (string)reader["Address"];
                    user.Phone = (string)reader["Phone"];
                    user.Mail = (string)reader["Mail"];
                    user.Birthday = reader["Birthday"] != DBNull.Value ? (DateTime?)reader["Birthday"] : null;
                    userStateId = (int)reader["UserStateId"]; //FK
                    userRoleId = (int)reader["UserRoleId"]; //FK
                    reader.Close();
                    user.UserState =new  UserStateService().GetUserStateById(userStateId);
                    user.UserRole =new  UserRoleService().GetUserRoleById(userRoleId);

                }
            }
            return user;
        }

        /// <summary>
        /// 根据登录名查询用户
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public  User GetAdminUserByLoginId(string loginId)
        {
            string sql = "SELECT Users.* FROM Users INNER JOIN UserRoles ON Users.UserRoleId=UserRoles.Id WHERE LoginId = @LoginId AND UserRoles.Name='" + UserRoles.管理员.ToString() + "' ";
            int userStateId;
            int userRoleId;
            User user = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@LoginId", loginId)))
            {
                if (reader.Read())
                {
                    user = new User();

                    user.Id = (int)reader["Id"];
                    user.LoginId = (string)reader["LoginId"];
                    user.LoginPwd = (string)reader["LoginPwd"];
                    user.Name = (string)reader["Name"];
                    user.Address = (string)reader["Address"];
                    user.Phone = (string)reader["Phone"];
                    user.Mail = (string)reader["Mail"];
                    user.Birthday = reader["Birthday"] != DBNull.Value ? (DateTime?)reader["Birthday"] : null;
                    userStateId = (int)reader["UserStateId"]; //FK
                    userRoleId = (int)reader["UserRoleId"]; //FK
                    reader.Close();
                    user.UserState = new UserStateService().GetUserStateById(userStateId);
                    user.UserRole =new  UserRoleService().GetUserRoleById(userRoleId);

                }
            }
            return user;
        }


        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public  void AddUser(User user)
        {
            string sql =
                "INSERT Users (LoginId, LoginPwd, Name, Address, Phone, Mail,Birthday, UserRoleId, UserStateId)" +
                "VALUES (@LoginId, @LoginPwd, @Name, @Address, @Phone, @Mail,@Birthday, @UserRoleId, @UserStateId)";
            sql += " ; SELECT @@IDENTITY";

            object time = DBNull.Value;
            if (user.Birthday.HasValue)
                time = user.Birthday;

            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@UserStateId", user.UserState.Id),    //FK
				new SqlParameter("@UserRoleId", user.UserRole.Id),      //FK
				new SqlParameter("@LoginId", user.LoginId),
				new SqlParameter("@LoginPwd", user.LoginPwd),
				new SqlParameter("@Name", user.Name),
				new SqlParameter("@Address", user.Address),
				new SqlParameter("@Phone", user.Phone),
				new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@Birthday",time)
			};
            user.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));

        }

        /// <summary>
        /// 查询所有普通用户
        /// </summary>
        /// <returns></returns>
        public  List<User> GetNormalUsers()
        {
            string sql = "SELECT * FROM users WHERE userstateid = " + Convert.ToByte(UserStates.正常);
            return GetUsersBySql(sql);
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public  List<User> GetUsers()
        {
            string sqlAll = "SELECT * FROM Users";
            return GetUsersBySql(sqlAll);
        }

        /// <summary>
        /// 依据sql语句查询用户
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        private  List<User> GetUsersBySql(string safeSql)
        {
            List<User> list = new List<User>();
            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    User user = new User();

                    user.Id = (int)row["Id"];
                    user.LoginId = (string)row["LoginId"];
                    user.LoginPwd = (string)row["LoginPwd"];
                    user.Name = (string)row["Name"];
                    user.Address = (string)row["Address"];
                    user.Phone = (string)row["Phone"];
                    user.Mail = (string)row["Mail"];
                    user.Birthday = row["Birthday"] != DBNull.Value ? (DateTime?)row["Birthday"] : null;
                    user.UserState =new  UserStateService().GetUserStateById((int)row["UserStateId"]); //FK
                    user.UserRole =new  UserRoleService().GetUserRoleById((int)row["UserRoleId"]); //FK

                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据id删除用户
        /// </summary>
        /// <param name="id"></param>
        public  bool DeleteUserById(int id)
        {
            string sql = @"DELETE OrderBook WHERE OrderID IN(SELECT Orders.Id FROM Orders INNER JOIN Users ON Orders.UserId=Users.Id WHERE UserId=@Id)
                           DELETE Orders where UserId=@Id
                           DELETE Users WHERE Id = @Id";
            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@Id", id)
			};
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;
        }


        /// <summary>
        /// 更改会员状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public  bool ModifyUserState(int id, UserStates state)
        {
            string sql = "Update users SET userstateid =" + Convert.ToByte(state) + " WHERE Id = @UserId";
           
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, new SqlParameter("@UserId", id)) > 0;

        }


        /// <summary>
        /// 根据id查询单个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  User GetUserById(int id)
        {
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            int userStateId;
            int userRoleId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    User user = new User();
                    user.Id = (int)reader["Id"];
                    user.LoginId = (string)reader["LoginId"];
                    user.LoginPwd = (string)reader["LoginPwd"];
                    user.Name = (string)reader["Name"];
                    user.Address = (string)reader["Address"];
                    user.Phone = (string)reader["Phone"];
                    user.Mail = (string)reader["Mail"];
                    user.Birthday = reader["Birthday"] != DBNull.Value ? (DateTime?)reader["Birthday"] : null;
                    userStateId = (int)reader["UserStateId"]; //FK
                    userRoleId = (int)reader["UserRoleId"]; //FK
                    reader.Close();
                    user.UserState =new  UserStateService().GetUserStateById(userStateId);
                    user.UserRole =new UserRoleService().GetUserRoleById(userRoleId);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        public  bool ModifyUser(User user)
        {
            string sql =
                "UPDATE Users " +
                "SET " +
                    "UserStateId = @UserStateId, " + //FK
                    "UserRoleId = @UserRoleId, " + //FK
                    "LoginId = @LoginId, " +
                    "LoginPwd = @LoginPwd, " +
                    "Name = @Name, " +
                    "Address = @Address, " +
                    "Phone = @Phone, " +
                    "Mail = @Mail, " +
                    "Birthday = @Birthday " +
                "WHERE Id = @Id";
            SqlParameter[] para = new SqlParameter[]
			{
				new SqlParameter("@Id", user.Id),
				new SqlParameter("@UserStateId", user.UserState.Id), //FK
				new SqlParameter("@UserRoleId", user.UserRole.Id), //FK
				new SqlParameter("@LoginId", user.LoginId),
				new SqlParameter("@LoginPwd", user.LoginPwd),
				new SqlParameter("@Name", user.Name),
				new SqlParameter("@Address", user.Address),
				new SqlParameter("@Phone", user.Phone),
				new SqlParameter("@Mail", user.Mail),
                new SqlParameter("@Birthday", user.Birthday)
			};
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;
        }

    }
}
