using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models
{
	public class Transaction : BaseEntity
	{
		[Key]
		public int ID { get; set; }

		public int? TicketID { get; set; }

		public int? BuyerID { get; set; } // user who bought the ticket

		public int? SellerID { get; set; } // user who sold the ticket

		[Required]
		public string PaymentMethod { get; set; }

		[Required]
		public decimal Price { get; set; }

		public virtual User Buyer { get; set; }
		public virtual User Seller { get; set; }
		public virtual Ticket Ticket { get; set; }

		public Transaction()
		{
		}
	}
}

