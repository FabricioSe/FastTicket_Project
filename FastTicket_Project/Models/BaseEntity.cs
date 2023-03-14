using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTicket_Project.Models
{
	public class BaseEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedAt { get; set; }

		public BaseEntity()
		{
		}
	}
}

