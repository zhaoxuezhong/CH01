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
    public class OrderService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="shoppingItems">商品项集合</param>
        /// <param name="user">客户</param>
        public void MakeOrder(List<ShoppingItem> shoppingItems, User user)
        {
            //计算总价
            decimal totalPrice = 0;
            foreach (ShoppingItem item in shoppingItems)
            {
                totalPrice += item.Quantity * item.Book.UnitPrice;
            }

            //添加 order
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.TotalPrice = totalPrice;
            order.User = user;
            AddOrder(order);


            //添加 orderbook
            if (order.Id > 0)
            {
                foreach (ShoppingItem item in shoppingItems)
                {
                    OrderBook oBook = new OrderBook();
                    oBook.Book = item.Book;
                    oBook.Order = order;
                    oBook.Quantity = item.Quantity;
                    oBook.UnitPrice = item.Book.UnitPrice;
                    AddOrderBook(oBook);
                }
            }
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="shoppingItems">商品项集合</param>
        /// <param name="user">客户</param>
        /// <param name="useTransaction">是否使用事务</param>
        public void MakeOrder(List<ShoppingItem> shoppingItems, User user, bool useTransaction)
        {

            using (SqlConnection conn =new SqlConnection(this.connection))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                //计算总价
                decimal totalPrice = 0;
                foreach (ShoppingItem item in shoppingItems)
                {
                    totalPrice += item.Quantity * item.Book.UnitPrice;
                }

                try
                {
                    //添加 order
                    string sql =
                        "INSERT Orders (OrderDate, UserId, TotalPrice)" +
                        "VALUES (@OrderDate, @UserId, @TotalPrice)";

                    sql += " ; SELECT @@IDENTITY";

                    SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@UserId", user.Id), //FK
					new SqlParameter("@OrderDate", DateTime.Now),
					new SqlParameter("@TotalPrice", totalPrice)
				};
                    SqlCommand cmd = new SqlCommand(sql, conn, trans);
                    cmd.Parameters.AddRange(para);
                    int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                    //添加 orderbook
                    if (orderId > 0)
                    {
                        cmd.CommandText =
                       "INSERT OrderBook (OrderID, BookID, Quantity,UnitPrice)" +
                       " VALUES (@OrderID, @BookID, @Quantity,@UnitPrice)";

                        foreach (ShoppingItem item in shoppingItems)
                        {
                            para = new SqlParameter[]
                        {
				            new SqlParameter("@OrderID", orderId), //FK
				            new SqlParameter("@BookID", item.Book .Id ), //FK
                            new SqlParameter("@Quantity", item.Quantity ),   
                            new SqlParameter("@UnitPrice", item.Book .UnitPrice )    
			            };
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(para);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception exe)
                {
                    trans.Rollback();
                    throw exe;
                }
            }
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public void AddOrder(Order order)
        {
            string sql =
                "INSERT Orders (OrderDate, UserId, TotalPrice)" +
                "VALUES (@OrderDate, @UserId, @TotalPrice)";

            sql += " ; SELECT @@IDENTITY";

            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@UserId", order.User.Id), //FK
					new SqlParameter("@OrderDate", order.OrderDate),
					new SqlParameter("@TotalPrice", order.TotalPrice)
				};

            order.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));
        }


        /// <summary>
        /// 添加订单中的图书
        /// </summary>
        /// <param name="order"></param>
        public void AddOrderBook(OrderBook order)
        {
            string sql =
                "INSERT OrderBook (OrderID, BookID, Quantity,UnitPrice)" +
                " VALUES (@OrderID, @BookID, @Quantity,@UnitPrice)";
            sql += " ; SELECT @@IDENTITY";
            SqlParameter[] para = new SqlParameter[]
            {
				new SqlParameter("@OrderID", order.Order.Id), //FK
				new SqlParameter("@BookID", order.Book.Id), //FK
                new SqlParameter("@Quantity", order.Quantity),   
                new SqlParameter("@UnitPrice", order.UnitPrice)    
			};

            order.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));
        }

        public Order GetOrderById(int id)
        {
            string sql = "SELECT * FROM Orders WHERE Id = @Id";

            int userId;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    Order order = new Order();
                    order.Id = (int)reader["Id"];
                    order.OrderDate = (DateTime)reader["OrderDate"];
                    order.TotalPrice = (decimal)reader["TotalPrice"];
                    userId = (int)reader["UserId"]; //FK
                    reader.Close();//必须显示关闭，后面要使用
                    order.User = new UserService().GetUserById(userId);
                    return order;
                }
            }
            return null;
        }


        public List<Order> GetOrders()
        {
            string sqlAll = "SELECT * FROM Orders";
            return GetOrdersBySql(sqlAll);
        }

        private List<Order> GetOrdersBySql(string safeSql)
        {
            List<Order> list = new List<Order>();
            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow row in dt.Rows)
                {
                    Order order = new Order();

                    order.Id = (int)row["Id"];
                    order.OrderDate = (DateTime)row["OrderDate"];
                    order.TotalPrice = (decimal)row["TotalPrice"];
                    order.User = new UserService().GetUserById((int)row["UserId"]); //FK
                    list.Add(order);
                }
            }

            return list;
        }

        /// <summary>
        /// 通过id获取订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderBook> GetOrderDetailById(int id)
        {
            List<OrderBook> list = new List<OrderBook>();
            string sql = "SELECT * FROM OrderBook WHERE OrderID = @OrderID";

            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, sql, new SqlParameter("@OrderID", id));
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    OrderBook book = new OrderBook();

                    book.Id = (int)dr["Id"];
                    book.Order = new OrderService().GetOrderById((int)dr["OrderID"]);
                    book.Quantity = (int)dr["Quantity"];
                    book.UnitPrice = (decimal)dr["UnitPrice"];
                    book.Book = new BookService().GetBookById((int)dr["BookID"]);
                    list.Add(book);
                }
            }
            return list;

        }


    }
}
