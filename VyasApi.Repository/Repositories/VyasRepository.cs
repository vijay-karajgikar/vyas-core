using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VyasApi.Repository.Repositories
{
	public abstract class VyasRepository<TEntity, TContext> : IVyasRepository<TEntity>
		where TEntity: class
		where TContext: DbContext
	{
		private readonly TContext dbContext;

		public VyasRepository(TContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<TEntity> Create(TEntity entity)
		{
			try
			{
				dbContext.Set<TEntity>().Add(entity);
				await dbContext.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<TEntity> Delete(long id)
		{
			try
			{
				var entity = await dbContext.Set<TEntity>().FindAsync(id);
				if (entity == null) return null;

				dbContext.Set<TEntity>().Remove(entity);
				await dbContext.SaveChangesAsync();

				return entity;
			}
			catch (Exception ex)
			{				
				throw ex;
			}
		}

		public async Task<List<TEntity>> Get()
		{
			try
			{
				return await dbContext.Set<TEntity>().ToListAsync();
			}
			catch (Exception ex)
			{				
				throw ex;
			}
		}

		public async Task<TEntity> Get(long id)
		{
			try
			{
				return await dbContext.Set<TEntity>().FindAsync(id);
			}
			catch (Exception ex)
			{				
				throw ex;
			}			
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			try
			{
				dbContext.Entry(entity).State = EntityState.Modified;
				await dbContext.SaveChangesAsync();
				return entity;	
			}
			catch (Exception ex)
			{				
				throw ex;
			}			
		}
	}
}