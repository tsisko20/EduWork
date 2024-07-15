using EduWork.Common.DTO;
using EduWork.Data;
using EduWork.Domain.Authentication;
using EduWork.Domain.Services;
using EduWork.WebAPI.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;

namespace EduWork.WebAPI.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection(AzureAdOptions.Section))
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddMicrosoftGraph(configuration.GetSection("MicrosoftGraph"))
                .AddInMemoryTokenCaches();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var modelState = actionContext.ModelState.Values;
                    return new BadRequestObjectResult(new ErrorModelDTO
                    {
                        Status = (int) HttpStatusCode.BadRequest,
                        Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                        Errors = modelState.SelectMany(x=>x.Errors, (x,y) =>y.ErrorMessage).ToList()

                    });
                };
            });
            services.AddScoped<WorkTimeService>();
            services.AddScoped<AppRoleService>();
            services.AddScoped<Identity>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(SwaggerOptions.Section).Bind(swaggerOptions);

            var azureAdOptions = new AzureAdOptions();
            configuration.GetSection(AzureAdOptions.Section).Bind(azureAdOptions);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Azure Entra", Version = "v1" });
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Oauth2.0 which uses AuthorizationCode flow",
                        Name = "oauth2.0",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(swaggerOptions.AuthorizationUrl),
                                TokenUrl = new Uri(swaggerOptions.TokenUrl),
                                Scopes = new Dictionary<string, string>
                                {
                        {swaggerOptions.Scope, "Access API as User" }
                                }
                            }
                        }
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                            },
                            new[]{ swaggerOptions.Scope }
                        }
                    });
                });
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpContextAccessor();
            services.AddTransient<IIdentity, Identity>();

            return services;
        }
    }
}
