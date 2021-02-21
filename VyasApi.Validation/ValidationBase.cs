using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using VyasApi.Data.Dto;

namespace VyasApi.Validation
{
	public abstract class ValidationBase<T> : AbstractValidator<T>, IValidatorInterceptor
	{
		public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext commonContext, ValidationResult result)
		{
			if (result.IsValid) return result;

			var errors = result.Errors.Select(x => new ErrorDto() 
            { 
                ErrorMessage = x.ErrorMessage, 
                PropertyName = x.PropertyName 
            });

            controllerContext.HttpContext.Items.Add("ValidationErrors", errors);

			return result;
		}

		public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext commonContext)
		{
			return commonContext;
		}
	}
}