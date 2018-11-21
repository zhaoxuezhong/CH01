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
    public class BookService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;
        public void AddBook(Book book)
        {
            string sql =
                "INSERT Books (Title, Author, PublisherId, PublishDate, ISBN,UnitPrice, ContentDescription,TOC,CategoryId)" +
                "VALUES (@Title, @Author, @PublisherId, @PublishDate, @ISBN, @UnitPrice, @ContentDescription, @TOC, @CategoryId)";

            sql += " ; SELECT @@IDENTITY";


            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@PublisherId", book.Publisher.Id), //FK
                    new SqlParameter("@CategoryId", book.Category.Id), //FK
                    new SqlParameter("@Title", book.Title),
                    new SqlParameter("@Author", book.Author),
                    new SqlParameter("@PublishDate", book.PublishDate),
                    new SqlParameter("@ISBN", book.ISBN),
                    new SqlParameter("@UnitPrice", book.UnitPrice),
                    new SqlParameter("@ContentDescription", book.ContentDescription),
                    new SqlParameter("@TOC", book.TOC),
                };
            book.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));

        }

        public bool ModifyBook(Book book)
        {
            string sql =
                "UPDATE Books " +
                "SET " +
                    "PublisherId = @PublisherId, " + //FK
                    "CategoryId = @CategoryId, " + //FK
                    "Title = @Title, " +
                    "Author = @Author, " +
                    "PublishDate = @PublishDate, " +
                    "ISBN = @ISBN, " +
                    "UnitPrice = @UnitPrice, " +
                    "ContentDescription = @ContentDescription, " +
                    "TOC = @TOC " +
                "WHERE Id = @Id";

            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", book.Id),
					new SqlParameter("@PublisherId", book.Publisher.Id), //FK
					new SqlParameter("@CategoryId", book.Category.Id), //FK
					new SqlParameter("@Title", book.Title),
					new SqlParameter("@Author", book.Author),
					new SqlParameter("@PublishDate", book.PublishDate),
					new SqlParameter("@ISBN", book.ISBN),
					new SqlParameter("@UnitPrice", book.UnitPrice),
					new SqlParameter("@ContentDescription", book.ContentDescription),
					new SqlParameter("@TOC", book.TOC)
				};

            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;

        }

        public List<Book> GetBooks()
        {
            string sqlAll = "SELECT * FROM Books";
            return GetBooksBySql(sqlAll);
        }

        public List<Book> GetNewBooks(int count)
        {
            string sqlAll = "SELECT TOP " + count + " * FROM Books ORDER BY PublishDate DESC";
            return GetBooksBySql(sqlAll);
        }

        public Book GetBookById(int id)
        {
            string sql = "SELECT * FROM Books WHERE Id = @Id";

            int publisherId;
            int categoryId;
            Book book = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    book = new Book();
                    book.Id = (int)reader["Id"];
                    book.Title = (string)reader["Title"];
                    book.Author = (string)reader["Author"];
                    book.PublishDate = (DateTime)reader["PublishDate"];
                    book.ISBN = (string)reader["ISBN"];
                    book.UnitPrice = (decimal)reader["UnitPrice"];
                    book.ContentDescription = (string)reader["ContentDescription"];
                    book.TOC = (string)reader["TOC"];
                    book.Clicks = (int)reader["Clicks"];
                    publisherId = (int)reader["PublisherId"]; //FK
                    categoryId = (int)reader["CategoryId"]; //FK
                    reader.Close();
                  

                    book.Publisher = new PublisherService().GetPublisherById(publisherId);
                    book.Category = new CategoryService().GetCategoryById(categoryId);
                   

                }
            }
            return book;

        }

        /// <summary>
        /// 获取分页列表的书籍
        /// </summary>
        /// <param name="categoryId">分类Id</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="currPageIndex">当前页索引</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="sortField">排序字段,允许的值为 PublishDate和 UnitPrice</param>
        /// <returns>泛型书籍集合</returns>
        public List<Book> GetBooks(int categoryId, int pageSize, int currPageIndex, ref int pageCount, string sortField)
        {
            List<Book> list = new List<Book>();
            SqlParameter para = new SqlParameter("@PageCount", pageCount);
            para.Direction = ParameterDirection.Output;
            SqlParameter[] paras = new SqlParameter[]
                {
                   
                    new SqlParameter("@SortField", sortField),
                    new SqlParameter("@CategoryId", categoryId ),
                    new SqlParameter("@PageSize", pageSize ),
                    new SqlParameter("@currPageIndex", currPageIndex),
                    para 
                 };

            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.StoredProcedure, "sp_QueryPagedBooks", paras);

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Book book = new Book();
                    book.Id = (int)row["Id"];
                    book.Title = (string)row["Title"];
                    book.Author = (string)row["Author"];

                    if (dt.Columns.Contains("UnitPrice"))
                        book.UnitPrice = (decimal)row["UnitPrice"];
                    if (dt.Columns.Contains("ShortContent"))
                        book.ContentDescription = (string)row["ShortContent"];
                    if (dt.Columns.Contains("ISBN"))
                        book.ISBN = (string)row["ISBN"];
                    if (dt.Columns.Contains("Clicks"))
                        book.Clicks = (int)row["Clicks"];
                    if (dt.Columns.Contains("PublishDate"))
                        book.PublishDate = (DateTime)row["PublishDate"];
                    if (dt.Columns.Contains("CategoryId"))
                        book.Category = new CategoryService().GetCategoryById((int)row["CategoryId"]);
                    book.Publisher = new PublisherService().GetPublisherById((int)row["PublisherId"]); //FK
                    list.Add(book);
                }
                pageCount = Convert.ToInt32(para.Value);
            }

            return list;
        }

        /// <summary>
        /// 获取按书籍列表
        /// </summary>
        /// <param name="category">查询类别</param>
        /// <param name="keyWord">关键字</param>
        /// <returns>泛型书籍集合</returns>
        public List<Book> GetBooks(BookQueryCategories category, string keyWord)
        {

            List<Book> list = new List<Book>();
            DataSet ds = SqlHelper.ExecuteDataset(this.connection, "sp_QueryBooks", category, keyWord);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Book book = new Book();

                    book.Id = (int)row["Id"];
                    book.Title = (string)row["Title"];
                    book.Author = (string)row["Author"];
                    book.PublishDate = (DateTime)row["PublishDate"];
                    book.ISBN = (string)row["ISBN"];
                    book.UnitPrice = (decimal)row["UnitPrice"];
                    book.ContentDescription = (string)row["ContentDescription"];
                    book.TOC = (string)row["TOC"];
                    book.Clicks = (int)row["Clicks"];
                    book.Publisher = new PublisherService().GetPublisherById((int)row["PublisherId"]); //FK
                    book.Category = new CategoryService().GetCategoryById((int)row["CategoryId"]); //FK
                    list.Add(book);
                }
            }
            return list;

        }

        public List<Book> GetRankings(int count)
        {
            string sql = "SELECT TOP " + count + " * FROM Books ORDER BY  Clicks DESC ";
            return this.GetBooksBySql(sql);
        }
        public bool DeleteBookById(int id)
        {
            string sql = "DELETE Books WHERE Id = @Id";
            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", id)
				};

            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;
        }

        /// <summary>
        /// 增加点击数
        /// </summary>
        /// <param name="id"></param>
        public bool AddClick(int id)
        {
            string sql = "UPDATE Books SET Clicks=Clicks+1 WHERE Id=" + id;
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql) > 0;
        }

        public List<Book> GetBooksBySql(string safeSql)
        {
            List<Book> list = new List<Book>();

            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    Book book = new Book();

                    book.Id = (int)row["Id"];
                    book.Title = (string)row["Title"];
                    book.Author = (string)row["Author"];
                    book.PublishDate = (DateTime)row["PublishDate"];
                    book.ISBN = (string)row["ISBN"];
                    book.UnitPrice = (decimal)row["UnitPrice"];
                    book.ContentDescription = (string)row["ContentDescription"];
                    book.TOC = (string)row["TOC"];
                    book.Clicks = (int)row["Clicks"];
                    book.Publisher = new PublisherService().GetPublisherById((int)row["PublisherId"]); //FK
                    book.Category = new CategoryService().GetCategoryById((int)row["CategoryId"]); //FK

                    list.Add(book);
                }
            }
            return list;

        }

    }
}