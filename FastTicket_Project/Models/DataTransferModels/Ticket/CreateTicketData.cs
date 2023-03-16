using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models.DataTransferModels.Ticket
{
	public class CreateTicketData
	{
		[Required]
		public decimal Price { get; set; }

		public bool OnSale { get; set; }

		public CreateTicketData()
		{
			OnSale = false;
		}
	}
}

