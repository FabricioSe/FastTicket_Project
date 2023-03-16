using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models.Entities
{
	public class Event
	{
        [Key]
        public int ID { get; set; }

		[Required]
        public DateTime CreatedAt { get; set; }

		[Required]
        public DateTime ModifiedAt { get; set; }

        [Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		// Address Details
		[Required]
		public string StreetNumber { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
		public DateTime Time { get; set; }

		[Required]
		public EventCategory Category { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }

		public Event()
		{
		}
	}
}

