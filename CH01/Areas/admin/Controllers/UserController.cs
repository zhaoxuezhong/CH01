using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CH01.Areas.admin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /admin/User/

        public ActionResult Login()
        {
            return View();
        }

    }
}
