using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Models
{
	[Serializable()]
	public class UserState
	{

		private int id; 
		private string name = String.Empty;

		public UserState() { }
        public UserState(int id) {
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

    public enum UserStates
    {
        正常=1,
        无效=2
    }

    public enum BookQueryCategories
    {
        书名,
        内容简介,
        出版社,
        作者
    }
}
