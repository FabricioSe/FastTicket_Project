using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FastTicket_Project.Models.Entities;

namespace FastTicket_Project.Models.DataTransferModels.Event
{
	public class CreateEventData
	{
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DisplayName("Street Number")]
        public string StreetNumber { get; set; }

        [Required]
        [DisplayName("Street Name")]
        public string StreetName { get; set; }

        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public EventCategory Category { get; set; }

        public CreateEventData()
		{
		}
	}
}

