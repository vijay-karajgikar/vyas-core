using System;
using FluentValidation;
using VyasApi.Data.Dto.User;
using VyasApi.Services.Interfaces;

namespace VyasApi.Validation.UserValidation
{
	public class AuthenticateUserValidation : ValidationBase<AuthenticateUserDto>
	{
		public AuthenticateUserValidation(IUserService userService, 
			IPasswordService passwordService)
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Invalid email address or password");
			
			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Invalid email address or password");

			RuleFor(x => x)
				.MustAsync(async (item, cancellationToken) => {					
					var exists = await userService.GetUserPassword(item);
					if (exists == null) return false;

					if (exists.Results == null) return false;
					if (!exists.Results.IsActive) return false;
					if (!exists.Results.Email.Equals(item.Email, StringComparison.OrdinalIgnoreCase)) return false;

					return passwordService.IsPasswordMatch(item.Password, exists.Results.Password);
				})
				.WithMessage("Invalid email address or password");
		}		
	}
}