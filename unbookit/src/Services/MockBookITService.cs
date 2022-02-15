namespace UnBookIT.Services;

public class MockBookITService : IBookITService
{
	public bool Delete(int id) =>
		id == 1;
}
