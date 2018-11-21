using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
	[Serializable()]
	public class Publisher
	{
		private int id; 
		private string name = String.Empty;
		public Publisher() { }
        public Publisher(int id, string name)
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
	}
}
