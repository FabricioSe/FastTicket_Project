using System;
using FastTicket_Project.Models;

namespace FastTicket_Project.DataSource
{
	public class Data
	{
		static Data instance;

		public List<Models.Category> myListCat = new List<Models.Category>();

        public List<string> catNamesSports = new List<string> { "Hockey Games", "Basketball", "Soccer", "UFC" };
        public List<string> catImagesSports = new List<string>();
        public List<string> catNamesConcerts = new List<string> { "Coldplay", "Muse", "Poets of the Fall", "Bruno Mars" };
        public List<string> catImagesConcets = new List<string>();
        public List<string> catNamesArt = new List<string> { "Movies", "Wizards and Jedi", "Live Art & Synths", "Kink Art 4" };
        public List<string> catImagesArt = new List<string>();

        public Data()
		{
            catImagesSports.Add("https://i.cbc.ca/1.6218891.1634772576!/fileImage/httpImage/1205094796.jpg");
            catImagesSports.Add("https://library.sportingnews.com/styles/crop_style_16_9_desktop_webp/s3/2023-03/nba-plain--6022a76f-1176-4256-ae79-fb77a9b7f298.png.webp?itok=sZRALOG5");
            catImagesSports.Add("https://imageio.forbes.com/specials-images/imageserve/628e7c0d5acb09ecc3d266ae/soccer-new/0x0.jpg?format=jpg&height=1080&width=1920");
            catImagesSports.Add("https://i.guim.co.uk/img/media/825aad01f71e8e91428629293c0ce015ddeb80ab/0_20_4847_2909/master/4847.jpg?width=620&quality=85&dpr=1&s=none");

            catImagesConcets.Add("https://i1.sndcdn.com/avatars-000237271265-7hhnty-t500x500.jpg");
            catImagesConcets.Add("https://i.discogs.com/fVP0SuOknUADW9xFscgdW-0PEh4q6C61GsdbRFtyqjQ/rs:fit/g:sm/q:90/h:384/w:600/czM6Ly9kaXNjb2dz/LWRhdGFiYXNlLWlt/YWdlcy9BLTEwMDMt/MTYyMjMwMTM0My05/NzI3LmpwZWc.jpeg");
            catImagesConcets.Add("https://i.ytimg.com/vi/di7NMssrqsE/maxresdefault.jpg");
            catImagesConcets.Add("https://hips.hearstapps.com/hmg-prod/images/gettyimages-134315104.jpg");

            catImagesArt.Add("https://mediafiles.cineplex.com/Central/Film/Posters/30872_320_470.jpg");
            catImagesArt.Add("https://lumiere-a.akamaihd.net/v1/images/5b4e039dcd17b90001c2e6e0-image_b2fdff7c.jpeg?region=0,0,1536,864");
            catImagesArt.Add("https://cdn-az.allevents.in/events5/banners/6698cc1700eff168f2a9ad9a908a16bfdc12764133ac9f8b47fd5bd2524944c6-rimg-w1200-h600-gmir.jpg?v=1672838589");
            catImagesArt.Add("https://img.evbuc.com/https%3A%2F%2Fcdn.evbuc.com%2Fimages%2F408132479%2F340927246415%2F1%2Foriginal.20221211-180345?w=600&auto=format%2Ccompress&q=75&sharp=10&rect=0%2C739%2C1728%2C864&s=960fabd07b0f4779abc895d0dc9d5587");

            myListCat.Add(new Category("Sports", catNamesSports, catImagesSports));
            myListCat.Add(new Category("Concerts", catNamesConcerts, catImagesConcets));
            myListCat.Add(new Category("Arts&Theatre", catNamesArt, catImagesArt));
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

