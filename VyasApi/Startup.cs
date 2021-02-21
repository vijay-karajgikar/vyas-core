using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VyasApi.Data;
using VyasApi.Data.Dto;
using VyasApi.Data.Dto.User;
using VyasApi.Repository.Contexts;
using VyasApi.Repository.Interfaces;
using VyasApi.Repository.Repositories;
using VyasApi.Services.Implementations;
using VyasApi.Services.Interfaces;
using VyasApi.Validation.UserValidation;

namespace VyasApi
{
	public class Startup
    {
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

        public void ConfigureServices(IServiceCollection services)
        {
			services.Configure<VyasConfig>(configuration);
            services.AddHttpContextAccessor();
            services.AddResponseCompression();
            services.AddMvc().AddFluentValidation();
			services.Configure<ApiBehaviorOptions>(options => 
			{
				options.InvalidModelStateResponseFactory = actionContext => 
				{
					var responseDto = new ResponseDto<object>() 
					{ 
						Errors = new List<ErrorDto>() 
						{ 
							new ErrorDto 
							{ 
								ErrorCode = 1, 
								ErrorMessage = "Request has one or more validation errors" 
							} 
						} 
					};										

					if (actionContext.ModelState.ErrorCount == 0) return new BadRequestObjectResult(responseDto);
					var exists = actionContext.HttpContext.Items.TryGetValue("ValidationErrors", out var validationErrors);
					if (exists)
					{
						responseDto.Errors = validationErrors as IEnumerable<ErrorDto>;
					}
					else
					{
						responseDto.Errors = actionContext.ModelState.Select(x => new ErrorDto
                        {
                            ErrorCode = 1,
                            PropertyName = x.Key,
                            ErrorMessage = string.Join(",", x.Value.Errors.Select(c => c.ErrorMessage))
                        });
					}
					return new BadRequestObjectResult(responseDto);
				};
			});

			services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("vyasdev")));
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IPasswordService, PasswordService>();

			services.AddTransient<IValidator<RegisterUserDto>, RegisterUserValidation>();
			services.AddTransient<IValidator<ActivateUserDto>, ActivateUserValidation>();
			services.AddTransient<IValidator<GenerateActivationCodeDto>, GenerateActivationCodeValidation>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
