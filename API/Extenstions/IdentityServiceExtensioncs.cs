using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace API.Extenstions
{
    public static class IdentityServiceExtensioncs
    {
        public static IServiceCollection  AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                var tokenKey = config["TokenKey"] ?? throw new Exception("Token not found");
                                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });
            return services;
        }
    }
}
