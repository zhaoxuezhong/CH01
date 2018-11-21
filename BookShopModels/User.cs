
using System;
namespace BookShop.Models
{

    [Serializable()]
	public class User
	{

        //用户状态类作为User属性的类型
        public UserState UserState { get; set; }
        //用户角色类作为User属性的类型
        public UserRole UserRole { get; set; }
        public int Id { get; set; }//编号
        public string LoginId { get; set; }//登录用户名
        public string LoginPwd { get; set; }//登录密码
        public string Name { get; set; }//姓名
        public string Address { get; set; }//地址
        public string Phone { get; set; }//电话
        public string Mail { get; set; }//邮箱
        public DateTime? Birthday { get; set; }  //出生日期


        
	}
}
