using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Models
{
    public class ShoppingItem
    {
        private Book book;

        public Book Book
        {
            get { return book; }
            set { book = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public ShoppingItem()
        {

        }

        public ShoppingItem(Book book, int quantity)
        {
            this.book = book;
            this.quantity = quantity;
        }
    }
}
