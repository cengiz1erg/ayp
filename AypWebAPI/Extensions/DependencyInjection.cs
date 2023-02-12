using AypWebAPI.Context;
using AypWebAPI.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace AypWebAPI.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFluentValidationRules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PostPlayerValidator>())
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdatePlayerValidator>())
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PatchPlayerBackNumberRequstValidator>());

            return services;
        }
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("MYdb"), ServiceLifetime.Transient);
            return services;
        }

    }
}
