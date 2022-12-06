using Microsoft.EntityFrameworkCore;
using UnBookIT.Models;

namespace UnBookIT.Data;

public class BookITContext : DbContext
{
	public BookITContext(DbContextOptions<BookITContext> options) : base(options) { }

	public DbSet<Booking> Bookings => Set<Booking>();
	public DbSet<PartyReport> PartyReports => Set<PartyReport>();
}
