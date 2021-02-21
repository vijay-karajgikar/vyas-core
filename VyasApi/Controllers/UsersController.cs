using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VyasApi.Data.Dto;
using VyasApi.Data.Dto.User;

namespace VyasApi.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private readonly IUserService userService;

		public UsersController(IUserService userService)
		{
			this.userService = userService;
		}


		[Route("registerUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto userDto)
        {
            try
			{
				var user = await userService.RegisterUser(userDto);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<List<UserDto>> 
					{
						Errors = new List<ErrorDto>
						{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
        }

		[Route("")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
			try
			{
				var users = await userService.GetAllUsers();
				return Ok(users);				
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<List<UserDto>> 
					{
						Errors = new List<ErrorDto>{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
        }

		[Route("activateUser")]
		[HttpPost]
		public async Task<IActionResult> ActivateUser([FromBody] ActivateUserDto userDto)
		{
			try
			{
				var user = await userService.ActivateUser(userDto);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<UserDto> 
					{
						Errors = new List<ErrorDto>
						{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
		}

		[Route("activationCode")]
		[HttpPost]
		public async Task<IActionResult> GenerateActivationCode(GenerateActivationCodeDto userDetails)
		{
			try
			{
				var activationCode = await userService.GenerateActivationCode(userDetails);
				return Ok(activationCode);
			}
			catch (System.Exception ex)
			{				
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<UserDto> 
					{
						Errors = new List<ErrorDto>
						{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
		}

		[Route("authenticate")]
		[HttpPost]
		public async Task<IActionResult> AuthenticateUser(AuthenticateUserDto userDto)
		{
			try
			{
				var authenticationResult = await userService.AuthenticateUser(userDto);
				return Ok(authenticationResult);
			}
			catch (System.Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<UserDto> 
					{
						Errors = new List<ErrorDto>
						{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
		}

		[Route("email")]
		[HttpGet]
		public async Task<IActionResult> GetUser([FromQuery] string email)
		{
			try
			{
				var users = await userService.GetUserByEmail(email);
				return Ok(users);				
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, 
					new ResponseDto<List<UserDto>> 
					{
						Errors = new List<ErrorDto>{
							new ErrorDto 
							{ ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
		}
		
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user)
        {
            try
			{
				var updated = await userService.UpdateUser(user);
				return Ok(updated);
			}
			catch (System.Exception ex)
			{				
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseDto<UserDto>
					{
						Errors = new List<ErrorDto> {
							new ErrorDto { ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
        }

		[HttpDelete]
		public async Task<IActionResult> DeleteUser([FromBody] UserDto user)
		{
			try
			{
				var deleteUserResult = await userService.DeleteUser(user.Email);
				return Ok(deleteUserResult);
			}
			catch (Exception ex)
			{				
				return StatusCode(StatusCodes.Status500InternalServerError,
					new ResponseDto<UserDto>
					{
						Errors = new List<ErrorDto> {
							new ErrorDto { ErrorCode = 1, ErrorMessage = ex.Message }
						}
					});
			}
		}
	}
}