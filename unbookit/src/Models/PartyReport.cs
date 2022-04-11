using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnBookIT.Models;

[Table("party_reports")]
public class PartyReport
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[ForeignKey(nameof(Booking))]
	[Column("booking_id")]
	public int BookingId { get; set; }
}
