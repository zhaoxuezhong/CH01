using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Models;
using BookShop.BLL;
using CH01.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace CH01.Controllers
{
    public class UserController : Controller
    {
        private UserManager um = new UserManager();
        //到登陆页面
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //登陆
        [HttpPost]
        public ActionResult DoLogin()
        {
            string userName = Request.Form["username"];
            string passwrod = Request.Form["password"];
            string remanber = Request.Form["remanber"];
            string validateCode = Request.Form["validateCode"];
            string securityCode = TempData["securityCode"]!=null? Convert.ToString(TempData["securityCode"]) :"";
            if (!securityCode.Equals(validateCode.ToUpper()))
            {
                return View("login");
            }
            User loginUser = null;
            if (um.LogIn(userName, passwrod,out loginUser))
            {
                Session["loginUser"] = loginUser;
                if (!string.IsNullOrEmpty(remanber))    //记住用户名密码
                {
                    HttpCookie nameCookie = new HttpCookie("userName", userName);
                    nameCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(nameCookie);
                    HttpCookie pwdCookie = new HttpCookie("password", passwrod);
                    pwdCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(pwdCookie);
                }
                else
                {
                    //让cookie失效
                    Response.Cookies["userName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);
                }
                return Redirect("~/home/index");
            }
            else
            {
                loginUser = null;
                return Redirect("~/user/login");
            }
        }

        //注销
        [HttpGet]
        public ActionResult LoginOut()
        {
            Session.Abandon();
            return Redirect("~/home/index");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoRegister(RegisterModel model)
        {
            User user = new User();
            user.LoginId = model.LoginId;
            user.LoginPwd = model.LoginPwd;
            user.Mail = model.Mail;
            user.UserRole=new UserRole(1);
            user.UserState = new UserState(1); ;
            user.Name = model.LoginId;
            user.Address = "";
            user.Phone = "";
            user.Birthday = DateTime.Now;
            um.Register(user);
            return Redirect("~/user/login");
        }

        public ActionResult SecurityCode()
        {
            String code = CreateRandomCode(6);
            TempData["securityCode"] = code;
            return File(CreateValidateGraphic(code), "image/Jpeg");
        }

        public string CreateRandomCode(int codeCount)
        {
            string chars = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,R,S,T,U,W,X,Y,Z";
            string[] allChar = chars.Split(',');
            String randomCode = "";
            Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                int t = random.Next(allChar.Length);
                randomCode += allChar[t];
            }
            return randomCode;
        }

        public byte[] CreateValidateGraphic(String validateCode)
        {
            Bitmap bitmap = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(bitmap);

            try
            {
                Random random = new Random();
                g.Clear(Color.White);

                //干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(bitmap.Width);
                    int x2 = random.Next(bitmap.Width);
                    int y1 = random.Next(bitmap.Height);
                    int y2 = random.Next(bitmap.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                }

                Font font = new Font("Arial", 13, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);

                //干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(bitmap.Width);
                    int y = random.Next(bitmap.Height);
                    bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                
                //边框
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                MemoryStream stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                bitmap.Dispose();
            }

        }

    }
}
