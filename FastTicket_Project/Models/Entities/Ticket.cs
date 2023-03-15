using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models.Entities
{
	public class Ticket
	{
        [Key]
        public int ID { get; set; }

		[Required]
        public DateTime CreatedAt { get; set; }

		[Required]
        public DateTime ModifiedAt { get; set; }

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

