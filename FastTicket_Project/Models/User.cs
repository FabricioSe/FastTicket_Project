using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FastTicket_Project.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public class User : BaseEntity
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		[Phone(ErrorMessage = "Invalid phone number")]
		public string Phone { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }

		public User()
		{
		}
	}
}

