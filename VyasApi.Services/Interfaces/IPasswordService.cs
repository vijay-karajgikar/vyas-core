namespace VyasApi.Services.Interfaces
{
	public interface IPasswordService
	{		
		string HashPassword(string password);
		bool IsPasswordMatch(string password, string dbPassword);
	}

}