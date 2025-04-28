using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.Application.Services;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers(options =>
    // {
    //     options.Filters.Add<ErrorHandlingFilterAttribute>();
    // });
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
