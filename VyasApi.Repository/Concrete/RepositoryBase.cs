using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VyasApi.Repository.Contexts;
using VyasApi.Repository.Interfaces;

namespace VyasApi.Repository.Concrete
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected UserDbContext RepositoryContext {get;set;}
		public RepositoryBase(UserDbContext repositoryContext)
		{
			RepositoryContext = repositoryContext;
		}

		public void Create(T entity)
		{
			RepositoryContext.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			RepositoryContext.Set<T>().Remove(entity);
		}

		public IQueryable<T> FindAll()
		{
			return this.RepositoryContext.Set<T>().AsNoTracking();
		}

		public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
		{
			return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
		}

		public void Update(T entity)
		{
			this.RepositoryContext.Set<T>().Update(entity);
		}
	}
}