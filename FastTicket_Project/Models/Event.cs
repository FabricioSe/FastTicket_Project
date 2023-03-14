using System;
using System.ComponentModel.DataAnnotations;

namespace FastTicket_Project.Models
{
	public class Event : BaseEntity
	{
		[Key]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Location { get; set; }

		[Required]
		public DateTime Time { get; set; }

		[Required]
		public EventCategory Category { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }

		public Event()
		{
		}
	}
}

