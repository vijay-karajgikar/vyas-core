using FluentValidation;
using VyasApi.Data.Dto.User;

namespace VyasApi.Validation.UserValidation
{
	public class RegisterUserValidation : ValidationBase<RegisterUserDto>
	{
		public RegisterUserValidation(IUserService userService)
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Email address is invalid")
				.MustAsync(async (item, email, cancellationToken) => {
					var exists = await userService.GetUserByEmail(item);
					return exists != null && exists.Results == null;
				})
				.WithMessage("Email address is invalid");

			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Full name is required");

			/*
			1. Must be at least 8 characters in length.
			2. Must contain at least 1 digit
			3. Must contain one lowercase letter
			4. Must contain one uppercase letter
			5. Must not contain any white spaces
			6. Must contain only these special characters
			   ! * ^ > + - _ @ # $ % & 
			*/
			RuleFor(x => x.Password)
				.NotEmpty()				
				.MinimumLength(8)
				.Matches(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s)[0-9a-zA-Z!*^?](?=.*[\+\-|_\@\#\$\%\&])")
				.WithMessage("Password is invalid");
		}
	}
}