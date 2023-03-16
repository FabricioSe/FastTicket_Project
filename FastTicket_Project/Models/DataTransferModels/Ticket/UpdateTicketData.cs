using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models.DataTransferModels.Ticket
{
	public class UpdateTicketData
	{
		[Required]
		public decimal Price { get; set; }

		[Required]
		public bool OnSale { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public int EventId { get; set; }

		public UpdateTicketData()
		{
		}
	}
}

