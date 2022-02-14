using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnBookIT.Models;

[Table("bookings")]
public class Booking
{
	[Key]
	[Column("id")]
	public int Id { get; set; }
}
