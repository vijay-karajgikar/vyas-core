using System.Text.RegularExpressions;
using VyasApi.Services.Interfaces;

namespace VyasApi.Services.Implementations
{
	public class PasswordService : IPasswordService
	{		
		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool IsPasswordMatch(string password, string dbPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, dbPassword);
		}
	}
}