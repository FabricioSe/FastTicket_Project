using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FastTicket_Project.Models.Entities
{
	public class Transaction
	{
        [Key]
        public int ID { get; set; }

		[Required]
        public DateTime CreatedAt { get; set; }

        public int? TicketID { get; set; }

		public string? BuyerID { get; set; } // user who bought the ticket

		public string? SellerID { get; set; } // user who sold the ticket

		public bool SellerReceivedBalance { get; set; }

		[Required]
		public decimal Price { get; set; }

		public virtual IdentityUser Buyer { get; set; }
		public virtual IdentityUser Seller { get; set; }
		public virtual Ticket Ticket { get; set; }

		public Transaction()
		{
		}
	}
}

