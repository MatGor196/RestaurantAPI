using RestaurantAPI;
using RestaurantAPI.Services;
using NLog.Web;
using RestaurantAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRestaurantHandler, RestaurantHandler>();
builder.Services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddDbContext<RestaurantDbContext>(); 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.WebHost.UseNLog();

var app = builder.Build();

using(var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var restaurantSeederService = services.GetRequiredService<IRestaurantSeeder>();
    restaurantSeederService.Seed();
}

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
    });
}

app.UseTiming();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandling();

app.MapControllers();

app.Run();
