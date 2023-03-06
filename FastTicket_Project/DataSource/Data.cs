using System;
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
			catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImages.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");

            myListCat.Add(new Models.Category("Sports", catImages, catNames));
            myListCat.Add(new Models.Category("Concerts", catImages, catNames));
            myListCat.Add(new Models.Category("Arts&Theatre", catImages, catNames));
            myListCat.Add(new Models.Category("Sports", catImages, catNames));
        }

		public static Data GetInstance()
		{
			if (GetInstance == null)
			{
				instance = new Data();
			}
			return instance;
		}
	}
}

