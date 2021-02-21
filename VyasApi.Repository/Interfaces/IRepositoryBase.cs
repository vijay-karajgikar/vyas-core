using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VyasApi.Repository.Interfaces
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindAll();
		IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}