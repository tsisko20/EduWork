using Swashbuckle.AspNetCore.Swagger;

namespace EduWork.WebAPI.Configurations
{
    public static class WebApplicationExtensions
    {
        public static WebApplication Configure(this WebApplication app, IWebHostEnvironment env, IConfiguration conf) 
        {
            if (env.IsDevelopment())
            {
                var swaggerOptions = new SwaggerOptions();
                conf.GetSection(SwaggerOptions.Section).Bind(swaggerOptions);
                app.UseSwagger();
                app.UseSwaggerUI(c=>
                {
                    c.OAuthClientId(swaggerOptions.ClientId);
                    c.OAuthUsePkce();
                });
            }
                
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
