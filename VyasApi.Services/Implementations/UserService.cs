using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VyasApi.Data.Dto;
using VyasApi.Data.Dto.User;
using VyasApi.Repository.Data;
using VyasApi.Repository.Interfaces;
using VyasApi.Services.Interfaces;

public class UserService : IUserService
{
	private readonly IPasswordService passwordService;
	private readonly IUserRepository userRepository;

	public UserService(
		IPasswordService passwordService,
		IUserRepository userRepository)
	{
		this.passwordService = passwordService;
		this.userRepository = userRepository;		
	}
	
	public async Task<ResponseDto<UserDto>> RegisterUser(RegisterUserDto userDto)
	{
		var userPassword = passwordService.HashPassword(userDto.Password);
		string activationId = GenerateActivationId();
		var userEntity = new User
		{
			CreatedDate = DateTime.Now,
			Email = userDto.Email,
			FullName = userDto.FullName,
			Token = userPassword,
			ActivationId = activationId
		};

		var savedUserEntity = await userRepository.CreateUser(userEntity);
		return new ResponseDto<UserDto>
		{
			Results = new UserDto
			{
				Email = savedUserEntity.Email,
				Id = savedUserEntity.Id,
				FullName = savedUserEntity.FullName,
				CreatedDate = savedUserEntity.CreatedDate,
				LastLogin = savedUserEntity.LastLogin,
				IsActive = savedUserEntity.IsActive,
				ActivationId = savedUserEntity.ActivationId
			}
		};
	}
	public async Task<ResponseDto<UserDto>> GetUserByEmail(UserDto userDto)
	{
		var user = await userRepository.GetUserByEmail(userDto.Email);
		if (user != null)
		{
			return new ResponseDto<UserDto>() {
				Results = new UserDto {
					Email = user.Email, 
					ActivationId = user.ActivationId, 
					CreatedDate = user.CreatedDate, 
					FullName = user.FullName, 
					Id = user.Id, 
					IsActive = user.IsActive, 
					LastLogin = user.LastLogin
				}
			};
		}
		return new ResponseDto<UserDto> { Results = null };
	}
	public async Task<ResponseDto<List<UserDto>>> GetAllUsers()
	{
		var users = await userRepository.GetAllUsersAsync();
		if (users != null && users.Any())
		{
			return new ResponseDto<List<UserDto>> {
				Results = users.Select(user => new UserDto{
						Email = user.Email, 
						ActivationId = user.ActivationId, 
						CreatedDate = user.CreatedDate, 
						FullName = user.FullName, 
						Id = user.Id, 
						IsActive = user.IsActive, 
						LastLogin = user.LastLogin
				}).ToList()
			};
		}
		return new ResponseDto<List<UserDto>> { Results = null };
	}
	public async Task<ResponseDto<UserDto>> ActivateUser(ActivateUserDto userDto)
	{
		var userEntity = await userRepository.GetUserByEmail(userDto.Email);
		if (userEntity != null)
		{
			userEntity.ActivationId = null;
			userEntity.IsActive = true;
		}
		var updatedUserEntity = await userRepository.UpdateUser(userEntity);
		if (updatedUserEntity != null)
		{
			return new ResponseDto<UserDto> {
				Results = new UserDto {
					Email = updatedUserEntity.Email,
					Id = updatedUserEntity.Id,
					FullName = updatedUserEntity.FullName,
					CreatedDate = updatedUserEntity.CreatedDate,
					LastLogin = updatedUserEntity.LastLogin,
					IsActive = updatedUserEntity.IsActive
				}
			};
		}
		return null;


	}
	public async Task<ResponseDto<UserDto>> GenerateActivationCode(GenerateActivationCodeDto userDto)
	{
		var userEntity = await userRepository.GetUserByEmail(userDto.Email);
		if (userEntity != null)
		{
			userEntity.ActivationId = GenerateActivationId();
			userEntity.IsActive = false;
			userEntity.CreatedDate = DateTime.Now;
		}
		var updatedUserEntity = await userRepository.UpdateUser(userEntity);
		if (updatedUserEntity != null)
		{
			return new ResponseDto<UserDto> {
				Results = new UserDto {
					Email = updatedUserEntity.Email,
					Id = updatedUserEntity.Id,
					FullName = updatedUserEntity.FullName,
					ActivationId = updatedUserEntity.ActivationId
				}
			};
		}
		return null;
	}
	public async Task<ResponseDto<UserDto>> AuthenticateUser(AuthenticateUserDto userDto)
	{
		return null;
	}
	public async Task<ResponseDto<UserDto>> GetUserPassword(UserDto userDto)
	{
		var user = await userRepository.GetUserByEmail(userDto.Email);
		if (user != null)
		{
			return new ResponseDto<UserDto>() {
				Results = new UserDto {
					Email = user.Email, 
					ActivationId = user.ActivationId, 
					CreatedDate = user.CreatedDate, 
					FullName = user.FullName, 
					Id = user.Id, 
					IsActive = user.IsActive, 
					LastLogin = user.LastLogin,
					Password = user.Token
				}
			};
		}
		return new ResponseDto<UserDto> { Results = null };
	}
	
	public async Task<ResponseDto<bool>> DeleteUser(string userEmail)
	{
		var userEntity = await userRepository.GetUserByEmail(userEmail);
		if (userEntity != null)
		{
			var deletedUser = await userRepository.DeleteUser(userEntity);
			return new ResponseDto<bool> { Results = deletedUser };
		}
		return new ResponseDto<bool> { Results = false };
	}

	private static string GenerateActivationId()
	{
		string allowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
		var bytes = new byte[100];
		using (var randomNumber = RandomNumberGenerator.Create())
		{
			randomNumber.GetBytes(bytes);
		}
		var activationId = new string(bytes.Select(x => allowableCharacters[x % allowableCharacters.Length]).ToArray());
		return activationId;
	}
}