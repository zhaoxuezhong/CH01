using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.BLL;
using BookShop.Models;

namespace CH01.Controllers
{
    public class HomeController : Controller
    {
        CategoryManager categoryManager = new CategoryManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            List<Category> list = categoryManager.GetCategories();
            return View(list);
        }

    }
}
