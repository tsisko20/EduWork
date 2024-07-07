using EduWork.Domain.Services;
using EduWork.WebAPI.Configurations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SwaggerOptions>(builder.Configuration.GetSection(SwaggerOptions.Section));

builder.Services.AddServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

app.Configure(app.Environment, builder.Configuration);

app.MapControllers();

app.Run();
