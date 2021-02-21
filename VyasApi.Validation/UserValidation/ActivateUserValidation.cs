using System;
using FluentValidation;
using Microsoft.Extensions.Options;
using VyasApi.Data;
using VyasApi.Data.Dto.User;

namespace VyasApi.Validation.UserValidation
{
	public class ActivateUserValidation : ValidationBase<ActivateUserDto>
	{
		public ActivateUserValidation(IUserService userService, IOptions<VyasConfig> options)
		{
			var config = options.Value;

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Email address is invalid")
				.MustAsync(async (item, email, cancellationToken) => {
					var exists = await userService.GetUserByEmail(item);
					if (exists.ResponseCode != 0) return false;
					if (exists.Results == null) return false;
					if (exists.Results.ActivationId != item.ActivationId) return false;
					if (!exists.Results.CreatedDate.HasValue) return false;
					
					var timeDifference = (DateTime.Now - exists.Results.CreatedDate.Value).TotalMinutes;
					return timeDifference <= config.ActivationPeriodInMins;
				})
				.WithMessage("Unable to activate the user or the activation period expired");
		}
	}
}