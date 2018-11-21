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
    public class SearchKeywordService
    {
        string connection = ConfigurationManager.ConnectionStrings["BookShop"].ConnectionString;

        public SearchKeyword GetKeyword(string keyword)
        {
            string sql = "SELECT * FROM SearchKeywords WHERE keyword = @keyword";
            SearchKeyword searchKeyword = null;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(this.connection, CommandType.Text, sql, new SqlParameter("@keyword", keyword)))
            {
                if (reader.Read())
                {
                    searchKeyword = new SearchKeyword();
                    searchKeyword.Id = (int)reader["Id"];
                    searchKeyword.Keyword = (string)reader["Keyword"];
                    searchKeyword.SearchCount = (int)reader["SearchCount"];

                }
            }
            return searchKeyword;
        }

        /// <summary>
        /// 搜索热门关键字
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="displaycount"></param>
        /// <returns></returns>
        public string[] GetHotSearchKeywords(string keyword, int displaycount)
        {
            List<SearchKeyword> keywords = new List<SearchKeyword>();
            List<string> results = new List<string>(displaycount);
            string sqlHot = "select top 10 * from SearchKeywords where keyword like '" + keyword + "%' order by SearchCount desc";
            keywords = GetSearchKeywordsBySql(sqlHot);
            foreach (SearchKeyword item in keywords)
            {
                results.Add(item.Keyword);
            }
            return results.ToArray();
        }


        private List<SearchKeyword> GetSearchKeywordsBySql(string safeSql)
        {
            List<SearchKeyword> list = new List<SearchKeyword>();

            DataSet ds = SqlHelper.ExecuteDataset(this.connection, CommandType.Text, safeSql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    SearchKeyword searchKeyword = new SearchKeyword();
                    searchKeyword.Id = (int)row["Id"];
                    searchKeyword.Keyword = (string)row["Keyword"];
                    searchKeyword.SearchCount = (int)row["SearchCount"];

                    list.Add(searchKeyword);
                }
            }
            return list;

        }

        public void AddSearchKeyword(SearchKeyword searchKeyword)
        {
            string sql =
                "INSERT SearchKeywords (Keyword, SearchCount)" +
                "VALUES (@Keyword, @SearchCount)";

            sql += " ; SELECT @@IDENTITY";


            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Keyword", searchKeyword.Keyword),
					new SqlParameter("@SearchCount", searchKeyword.SearchCount)
				};

            searchKeyword.Id = Convert.ToInt32(SqlHelper.ExecuteScalar(this.connection, CommandType.Text, sql, para));

        }

        public bool ModifySearchKeyword(SearchKeyword searchKeyword)
        {
            string sql =
                "UPDATE SearchKeywords " +
                "SET " +
                    "Keyword = @Keyword, " +
                    "SearchCount = @SearchCount " +
                "WHERE Id = @Id";

            SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@Id", searchKeyword.Id),
					new SqlParameter("@Keyword", searchKeyword.Keyword),
					new SqlParameter("@SearchCount", searchKeyword.SearchCount)
				};
            return SqlHelper.ExecuteNonQuery(this.connection, CommandType.Text, sql, para) > 0;

        }

    }
}
