using System;
namespace FastTicket_Project.Models
{
	public class Buyer
	{
		public string Email { get; set; }
		public string Password { get; set; }
        public Buyer() { }

		public Buyer(string email, string password)
		{
			Email = email;
			Password = password;
		}
	}
}

