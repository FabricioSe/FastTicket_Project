using System;
using FastTicket_Project.Models;

namespace FastTicket_Project.DataSource
{
	public class Data
	{
		static Data instance;

		public List<Models.Category> myListCat = new List<Models.Category>();
		public List<string> catImages = new List<string>();
		public List<string> catNames = new List<string> {"Hockey Games","Basketball", "Soccer", "UFC"};

		public Data()
		{
			catImages.Add("https://i.kinja-img.com/gawker-media/image/upload/t_original/ijsi5fzb1nbkbhxa2gc1.png");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");

            myListCat.Add(new Category("Sports", catNames, catImages));
            myListCat.Add(new Category("Concerts", catNames, catImages));
            myListCat.Add(new Category("Arts&Theatre", catNames, catImages));
            myListCat.Add(new Category("Sports", catNames, catImages));
        }

		public static Data GetInstance()
		{
			if (instance == null)
			{
				instance = new Data();
			}
			return instance;
		}
	}
}

