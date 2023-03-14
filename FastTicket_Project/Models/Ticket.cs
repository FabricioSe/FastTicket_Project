using System;
using System.ComponentModel.DataAnnotations;

namespace FastTicket_Project.Models
{
	public class Ticket : BaseEntity
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public int UserID { get; set; } // owner of ticket

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int EventID { get; set; }

		public bool OnSale { get; set; }

		public Ticket()
		{
			OnSale = false;
		}
	}
}

