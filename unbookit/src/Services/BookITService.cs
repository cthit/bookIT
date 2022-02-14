using UnBookIT.Data;
using UnBookIT.Models;

namespace UnBookIT.Services;

public class BookITService : IBookITService
{
	private readonly BookITContext context;

	public BookITService(BookITContext context)
	{
		this.context = context;
	}

	public bool Delete(int id)
	{
		if (context.Bookings.Find(id) is Booking booking)
		{
			context.Bookings.Remove(booking);
			context.SaveChanges();
			return true;
		}
		else
		{
			return false;
		}
	}
}
