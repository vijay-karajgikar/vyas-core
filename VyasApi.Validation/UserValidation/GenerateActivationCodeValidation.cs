using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using VyasApi.Data.Dto.User;
using VyasApi.Services.Interfaces;

namespace VyasApi.Validation.UserValidation
{
	public class GenerateActivationCodeValidation : ValidationBase<GenerateActivationCodeDto>
	{
		public GenerateActivationCodeValidation(
			IUserService userService,
			IPasswordService passwordService)
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Email address is invalid")
				.MustAsync(async (item, email, cancellationToken) => {
					var exists = await userService.GetUserPassword(item);
					if (exists.ResponseCode != 0) return false;
					if (exists.Results == null) return false;
					if (exists.Results.IsActive == true) return false;
					return passwordService.IsPasswordMatch(item.Password, exists.Results.Password);
				})
				.WithMessage("Email address is invalid");

		}		
	}
}