using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.BLL;
using BookShop.Models;

namespace CH01.Controllers
{
    public class BooksController : Controller
    {
        BookManager bookManager = new BookManager();
        //
        // GET: /Books/

        public ActionResult Detail()
        {
            int id=Convert.ToInt32(RouteData.Values["id"].ToString());
            Book book = bookManager.GetBookById(id);
            return View(book);
        }

        public ActionResult List()
        {
            int pageCount=0;
            int categoryId=Request.QueryString["categoryId"] != null ? 
                Convert.ToInt32(Request.QueryString["categoryId"]) : 1;
            int pageIndex = Request.QueryString["pageIndex"] != null ?
                Convert.ToInt32(Request.QueryString["pageIndex"]) : 1;
            int pageSize = Request.QueryString["pageSize"] != null ?
                Convert.ToInt32(Request.QueryString["pageSize"]) : 10;
            string order = Request.QueryString["order"] != null ?
                Convert.ToString(Request.QueryString["order"]) : "PublishDate";
            List<Book> bookList = bookManager.GetBooks(categoryId,pageSize,pageIndex,ref pageCount,order);

            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.order = order;
            ViewBag.categoryId = categoryId;

            return View(bookList);
        }

    }
}
