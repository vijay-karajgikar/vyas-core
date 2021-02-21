using System.Collections.Generic;
using System.Threading.Tasks;
using VyasApi.Data.Dto;
using VyasApi.Data.Dto.User;

public interface IUserService 
{
	Task<ResponseDto<UserDto>> RegisterUser(RegisterUserDto userDto);
	Task<ResponseDto<List<UserDto>>> GetAllUsers();
	Task<ResponseDto<UserDto>> ActivateUser(ActivateUserDto userDto);
	Task<ResponseDto<UserDto>> GenerateActivationCode(GenerateActivationCodeDto userDto);
	Task<ResponseDto<UserDto>> AuthenticateUser(AuthenticateUserDto userDto);
	Task<ResponseDto<UserDto>> GetUserByEmail(UserDto userDto);
	Task<ResponseDto<bool>> DeleteUser(string userEmail);
	Task<ResponseDto<UserDto>> GetUserPassword(UserDto userDto);
}