
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Models
{
	[Serializable()]
	public class UserRole
	{
		private int id; 
		private string name = String.Empty;

		public UserRole() { }
        public UserRole(int id) {
            this.id = id;
        }
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}		
		
	}

    public enum UserRoles
    {
        普通用户,
        VIP用户,
        管理员
    }
}
