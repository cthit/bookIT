using System.Threading.Tasks;

namespace UnBookIT.Services;

public interface IBookITService
{
	Task<bool> Delete(int id);
}
