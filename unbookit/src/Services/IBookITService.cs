using System.Collections.Generic;
using UnBookIT.Models;

namespace UnBookIT.Services;

public interface IBookITService
{
	IEnumerable<Booking> GetAll();

	Booking? Get(int id);

	bool Delete(int id);
}
