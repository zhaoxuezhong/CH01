using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.DAL;
using BookShop.Models;

namespace BookShop.BLL
{
    public class TemporaryCartManager
    {
        public bool AddItem(int bookId, int userId)
        {
            return new TemporaryCartService().AddItem(bookId, userId);
        }

        public bool RemoveItem(int id)
        {
            return new TemporaryCartService().RemoveItem(id);
        }

        public List<TemporaryCart> GetItems(int userId)
        {
            return new TemporaryCartService().GetItems( userId);
        }

        public bool ExistItem(int userId, int bookId)
        {
            return new TemporaryCartService().ExistItem(userId, bookId);
        }
    }
}
