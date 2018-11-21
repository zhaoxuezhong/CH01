using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookShop.Models;
using BookShop.DAL;
namespace BookShop.BLL
{
    public class CategoryManager
    {
        public  List<Category> GetCategories()
        {
            return new CategoryService().GetCategories();
        }

        public  void AddCategory(Category category)
        {
            new CategoryService().AddCategory(category);
        }
    }
}
