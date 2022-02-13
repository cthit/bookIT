using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnBookIT.Models;

[Table("bookings")]
public class Booking
{
	[Key]
	[Column("id")]
	public int Id { get; set; }

	[MaxLength(255)]
	[Column("user_id")]
	public string? UserId { get; set; }

	[Column("begin_date")]
	public DateTime? BeginDate { get; set; }

	[Column("end_date")]
	public DateTime? EndDate { get; set; }

	[MaxLength(255)]
	[Column("group")]
	public string? Group { get; set; }

	[MaxLength(65535)]
	[Column("description")]
	public string? Description { get; set; }

	[Column("room_id")]
	public int? RoomId { get; set; }

	[Column("created_at")]
	public DateTime? CreatedAt { get; set; }

	[Column("updated_at")]
	public DateTime? UpdatedAt { get; set; }

	[MaxLength(255)]
	[Column("title")]
	public string? Title { get; set; }

	[MaxLength(255)]
	[Column("phone")]
	public string? Phone { get; set; }

	[Column("deleted_at")]
	public DateTime? DeletedAt { get; set; }
}
