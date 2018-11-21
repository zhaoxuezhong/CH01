using System;
using System.Collections.Generic;
using System.Text;
using BookShop.DAL;
using BookShop.Models;

namespace BookShop.BLL
{

    public  class PublisherManager
    {
        public  List<Publisher> GetPublishers()
        {
            return new PublisherService().GetPublishers();
        }

        public  Publisher GetPublisherById(int id)
        {
            return new PublisherService().GetPublisherById(id);
        }

    }
}
