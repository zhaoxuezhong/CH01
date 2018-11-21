using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;

namespace BookShop.BLL
{
    public class UserManager
    {

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <param name="validUser">输出用户</param>
        /// <returns>返回true表示成功</returns>
        public bool LogIn(string loginId, string loginPwd, out User validUser)
        {
            User user =new UserService().GetUserByLoginId(loginId);
            if (user == null)
            {
                validUser = null;//用户名不存在 
                return false;
            }

            if (user.LoginPwd == loginPwd)
            {
                validUser = user;
                return true;
            }
            else
            {
                validUser = null;//密码错误 
                return false;
            }
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Register(User user)
        {
            if (LoginIdExists(user.LoginId))
            {
                return false;
            }
            else
            {
                AddUser(user);
                return true;
            }
        }


        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginPwd"></param>
        /// <param name="validUser"></param>
        /// <returns></returns>
        public  bool AdminLogin(string loginId, string loginPwd, out User validUser)
        {
            User user = new UserService().GetAdminUserByLoginId(loginId);

            if (user == null)
            {
                validUser = null;//用户名不存在 
                return false;
            }


            if (user.LoginPwd == loginPwd)
            {
                validUser = user;
                return true;
            }
            else
            {
                validUser = null;//密码错误 
                return false;
            }
        }


        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public bool LoginIdExists(string loginId)
        {
            if (new UserService().GetUserByLoginId(loginId) == null)
                return false;
            return true;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void AddUser(User user)
        {
            if (user.UserState == null)
            {
                user.UserState = new UserStateManager().GetDefaultUserState();
            }

            if (user.UserRole == null)
            {
                user.UserRole = new UserRoleManager().GetDefaultUserRole();
            }

            new UserService().AddUser(user);
        }

        public bool ModifyUser(User user)
        {
            return new UserService().ModifyUser(user);
        }

        /// <summary>
        /// 获得所有普通用户
        /// </summary>
        /// <returns></returns>
        public List<User> GetNormalUsers()
        {
            return new UserService().GetNormalUsers();
        }


        public List<User> GetUsers()
        {
            return new UserService().GetUsers();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteUserById(int id)
        {
            return new UserService().DeleteUserById(id);
        }

        /// <summary>
        /// 根据id修改用户状态
        /// </summary>
        /// <param name="userId"></param>
        public bool ModifyUserState(int userId)
        {

            User user = new UserService().GetUserById(userId);
            UserStates state = UserStates.正常;
            if (user.UserState.Id == Convert.ToInt32(UserStates.正常))
            {
                state = UserStates.无效;
            }
            else
            {
                state = UserStates.正常;
            }

            return new UserService().ModifyUserState(userId, state);
        }

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        public bool ModifyUserState(int userId, UserStates state)
        {
            return new UserService().ModifyUserState(userId, state);
        }

        public User  GetUserById(int id)
        {
         return   new UserService().GetUserById(id);
        }


        public User GetUserByLoginId(string loginId)
        {
            return new UserService().GetUserByLoginId(loginId);
        }
    }
}
