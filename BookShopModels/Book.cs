using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookShop.Models
{
	[Serializable()]
	public class Book
	{
		private int id; 
		private Publisher publisher; 
		private Category category; 
		private string title = String.Empty;
		private string author = String.Empty;
		private DateTime publishDate;
		private string iSBN = String.Empty;
		private decimal unitPrice;
		private string contentDescription = String.Empty;
		private string tOC = String.Empty;
		private int clicks;

		public Book() { }
		
		public int Id
		{
			get { return this.id; }
			set { this.id = value; }
		}

       
		public Publisher Publisher
		{
			get { return this.publisher; }
			set { this.publisher = value; }
		}

      
		public Category Category
		{
			get { return this.category; }
			set { this.category = value; }
		}

     
		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

     
		public string Author
		{
			get { return this.author; }
			set { this.author = value; }
		}

     
		public DateTime PublishDate
		{
			get { return this.publishDate; }
			set { this.publishDate = value; }
		}

     
		public string ISBN
		{
			get { return this.iSBN; }
			set { this.iSBN = value; }
		}

       
		public decimal UnitPrice
		{
			get { return this.unitPrice; }
			set { this.unitPrice = value; }
		}		

		public string ContentDescription
		{
			get { return this.contentDescription; }
			set { this.contentDescription = value; }
		}

		public string TOC
		{
			get { return this.tOC; }
			set { this.tOC = value; }
		}		

		public int Clicks
		{
			get { return this.clicks; }
			set { this.clicks = value; }
		}		
		
	}
}
