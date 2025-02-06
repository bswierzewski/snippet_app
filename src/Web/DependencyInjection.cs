using Application.Common.Interfaces;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Web.Infrastructure;
using Web.Services;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddFluentValidationRulesToSwagger();

        // Authentication and Authorization
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Auth0:Authority"]; // Adres URL Auth0
                options.Audience = configuration["Auth0:Audience"]; // Audience zdefiniowane w Auth0

                // Konfiguracja weryfikacji tokenu
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Weryfikuj wystawcê tokenu
                    ValidateAudience = true, // Weryfikuj odbiorcê tokenu
                    ValidateLifetime = true, // SprawdŸ, czy token nie wygas³
                    ValidateIssuerSigningKey = true, // Weryfikuj podpis tokenu
                    ValidIssuer = configuration["Auth0:Authority"], // Wystawca tokenu
                    ValidAudience = configuration["Auth0:Audience"], // Odbiorca tokenu
                };
            });
        services.AddAuthorization();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => 
            options.SuppressModelStateInvalidFilter = true);

        services.AddHttpContextAccessor();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddScoped<IUser, CurrentUser>();

        return services;
    }
}