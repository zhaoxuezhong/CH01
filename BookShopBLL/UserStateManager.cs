using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;
namespace BookShop.BLL
{
    public class UserStateManager
    {
        public UserState GetDefaultUserState()
        {
            return GetUserStateById(Convert.ToInt32(UserStates.正常));
        }

        public UserState GetUserStateById(int id)
        {
            return new UserStateService().GetUserStateById(id);
        }
    }
}
