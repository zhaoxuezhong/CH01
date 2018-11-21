using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.DAL;
using BookShop.Models;

namespace BookShop.BLL
{
    public class ShoppingManager
    {

        private List<ShoppingItem> shoppingItems;
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }



        public List<ShoppingItem> ShoppingItems
        {
            get
            {
                if (this.shoppingItems == null)
                    this.shoppingItems = new List<ShoppingItem>();
                return this.shoppingItems;
            }
            set { this.shoppingItems = value; }
        }

        public ShoppingManager()
        {

        }

        public ShoppingManager(object shoppingItems)
        {
            this.ShoppingItems = shoppingItems as List<ShoppingItem>;
        }

        public ShoppingManager(object shoppingItems, object user)
        {
            this.ShoppingItems = shoppingItems as List<ShoppingItem>;
            this.User = user as User;
        }

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bookId"></param>
        public void AddItem(int bookId)
        {
            bool hadBuy = false;
            foreach (ShoppingItem item in this.ShoppingItems)
            {
                if (item.Book.Id == bookId)
                {
                    hadBuy = true;
                    item.Quantity += 1;
                    break;
                }
            }
            if (!hadBuy)
            {
                Book book = new BookService().GetBookById(bookId);
                this.ShoppingItems.Add(new ShoppingItem(book, 1));
            }
        }

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="bookId"></param>
        public void RemoveItem(int bookId)
        {
            //foreach (ShoppingItem item in this.shoppingItems)
            //{
            //    if (item.Book.Id == bookId)
            //    {
            //        this.shoppingItems.Remove(item);
            //        break;
            //    }
            //}
            for (int i = 0; i < this.ShoppingItems.Count; i++)
            {
                if (this.ShoppingItems[i].Book.Id == bookId)
                {
                    this.ShoppingItems.Remove(this.ShoppingItems[i]);
                }
            }
        }


        /// <summary>
        /// 更新购买书籍数量
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="quantity"></param>
        public void UpdateQuantity(int bookId, int quantity)
        {
            foreach (ShoppingItem item in this.ShoppingItems)
            {
                if (item.Book.Id == bookId)
                {
                    item.Quantity = quantity;
                    break;
                }
            }
        }

        /// <summary>
        /// 由购物车生成订单
        /// </summary>
        public void MakeOrder()
        {
            if (this.user != null && this.ShoppingItems.Count > 0)
                new OrderService().MakeOrder(this.ShoppingItems, this.user,true);
        }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (ShoppingItem item in this.ShoppingItems)
                {
                    totalPrice += item.Quantity * item.Book.UnitPrice;
                }
                return totalPrice;
            }
        }

    }
}
