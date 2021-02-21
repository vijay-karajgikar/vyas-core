using System.Collections.Generic;
using System.Threading.Tasks;

namespace VyasApi.Repository
{
	public interface IVyasRepository<T> where T: class
	{
		Task<List<T>> Get();
		Task<T> Get(long id);
		Task<T> Create(T entity);
		Task<T> Update(T entity);
		Task<T> Delete(long id);
	}
}