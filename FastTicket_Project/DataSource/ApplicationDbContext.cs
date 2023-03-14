using System;
using FastTicket_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace FastTicket_Project.DataSource
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}

