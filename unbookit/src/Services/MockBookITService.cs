using System.Threading.Tasks;

namespace UnBookIT.Services;

public class MockBookITService : IBookITService
{
	public Task<bool> Delete(int id) =>
		Task.FromResult(id == 1);
}
