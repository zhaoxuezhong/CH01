using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;

namespace BookShop.BLL
{
    public class OrderManage
    {

        public  List<Order> GetOrders()
        {
            return new OrderService().GetOrders();
        }

        public  List<OrderBook> GetOrderDetailById(int id)
        {
            return new OrderService().GetOrderDetailById(id);
        }
    }
}
