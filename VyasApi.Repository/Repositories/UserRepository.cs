
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VyasApi.Repository.Concrete;
using VyasApi.Repository.Contexts;
using VyasApi.Repository.Data;
using VyasApi.Repository.Interfaces;

namespace VyasApi.Repository.Repositories
{	
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(UserDbContext repositoryContext) : base(repositoryContext)
		{			
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<User> GetUserById(long id)
		{
			return await FindBy(x => x.Id.Equals(id))
				.FirstOrDefaultAsync();
		}

		public async Task<User> GetUserByEmail(string email)
		{
			return await Task.Run(() => FindBy(x => x.Email.Equals(email)).FirstOrDefault());
		}

		public async Task<User> CreateUser(User user)
		{
			try
			{
				Create(user);
				await RepositoryContext.SaveChangesAsync();
				return await GetUserByEmail(user.Email);				
			}
			catch (Exception ex)
			{				
				throw ex;
			}
		}

		public async Task<User> UpdateUser(User user)
		{
			try
			{
				Update(user);
				await RepositoryContext.SaveChangesAsync();
				return await GetUserByEmail(user.Email);				
			}
			catch (Exception ex)
			{				
				throw ex;
			}
		}

		public async Task<bool> DeleteUser(User user)
		{
			try
			{
				Delete(user);
				await RepositoryContext.SaveChangesAsync();
				return true;	
			}
			catch (Exception ex)
			{				
				throw ex;
			}			
		}
	}
}