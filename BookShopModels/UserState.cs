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
        ����=1,
        ��Ч=2
    }

    public enum BookQueryCategories
    {
        ����,
        ���ݼ��,
        ������,
        ����
    }
}
