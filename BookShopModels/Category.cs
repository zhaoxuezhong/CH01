using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookShop.Models
{
	[Serializable()]
	public class Category
	{
		private int id; 
		private string name = String.Empty;
        private int pId;
        //private int sortNum;
		public Category() { }
        public Category(int id,string name)
        {
            this.id = id;
            this.name = name;
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

        public int PId
        {
            get { return pId; }
            set { pId = value; }
        }

        //public int SortNum
        //{
        //    get { return sortNum; }
        //    set { sortNum = value; }
        //}
	}
}
