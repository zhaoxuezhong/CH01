using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CH01.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="5-12个字符或数字组成")]
        public string LoginId { get; set; }//登录用户名
        [Required(ErrorMessage = "请输入密码")]
        public string LoginPwd { get; set; }//登录密码
        [Required(ErrorMessage = "请再次输入密码")]
        [Compare("LoginPwd",ErrorMessage="两次密码不一致")]
        public string rePwd { get; set; }//登录密码
        [Required(ErrorMessage = "请输入电子邮件")]
        public string Mail { get; set; }//邮箱
    }
}