using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.DAL;
using BookShop.Models;

namespace BookShop.BLL
{
    public class SearchKeywordManager
    {
        public void Search(string keyword)
        {
            SearchKeywordService service = new SearchKeywordService();
            SearchKeyword skw = service.GetKeyword(keyword);
            if (skw == null)
            {
                skw = new SearchKeyword();
                skw.Keyword = keyword;
                skw.SearchCount = 1;
                service.AddSearchKeyword(skw);
            }
            else
            {
                skw.SearchCount++;
                service.ModifySearchKeyword(skw);
            }
        }

        public string[] GetHotSearchKeywords(string keyWord, int displaycount)
        {
            SearchKeywordService service = new SearchKeywordService();
            return service.GetHotSearchKeywords(keyWord, displaycount);
        }
    }
}
