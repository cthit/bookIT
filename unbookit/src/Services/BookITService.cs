using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

	public async Task<bool> Delete(int id)
	{
		if (await context.Bookings.Include(b => b.PartyReports).SingleOrDefaultAsync(b => b.Id == id) is Booking booking)
		{
			foreach (var partyReport in booking.PartyReports)
			{
				context.PartyReports.Remove(partyReport);
			}
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
