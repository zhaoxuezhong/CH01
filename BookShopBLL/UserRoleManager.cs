using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;

namespace BookShop.BLL
{
    public class UserRoleManager
    {
        public  UserRole GetUserRoleById(int id)
        {
            return new UserRoleService().GetUserRoleById(id);
        }

        public  UserRole GetDefaultUserRole()
        {
            return GetUserRoleById(1);
        }
    }
}
