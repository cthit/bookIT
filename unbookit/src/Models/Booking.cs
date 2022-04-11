using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnBookIT.Models;

[Table("bookings")]
public class Booking
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	public ICollection<PartyReport> PartyReports { get; set; } = new HashSet<PartyReport>();
}
