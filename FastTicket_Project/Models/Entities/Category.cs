using System;
namespace FastTicket_Project.Models.Entities
{
	public class Category
	{
		public string Name { get; set; }
		public List<string> categoryNames { get; set; }
		public List<string> categoryImgs { get; set; }

		public Category() { }

		public Category(string name, List<string> categorynames, List<string> categoryimages)
		{
			Name = name;
			categoryNames = categorynames;
			categoryImgs = categoryimages;
		}
	}
}

