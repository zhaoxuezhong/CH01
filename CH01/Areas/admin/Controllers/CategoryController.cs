using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.BLL;
using BookShop.Models;

namespace CH01.Areas.admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager();

        public ActionResult Category()
        {
            List<Category> list = categoryManager.GetCategories();
            return View(list);
        }

        public ActionResult doAdd(String name)
        {
            Category category = new Category();
            category.Name = name;
            categoryManager.AddCategory(category);
            return Redirect("~/admin/category/category");
        }

    }
}
