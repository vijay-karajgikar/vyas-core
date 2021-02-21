using System.Collections.Generic;
using System.Threading.Tasks;
using VyasApi.Repository.Data;

namespace VyasApi.Repository.Interfaces
{
	public interface IUserRepository : IRepositoryBase<User>
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> GetUserById(long id);
		Task<User> GetUserByEmail(string email);
		Task<User> CreateUser(User user);
		Task<User> UpdateUser(User user);
		Task<bool> DeleteUser(User user);
	}
}