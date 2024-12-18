using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
 using Microsoft.Extensions.DependencyInjection;
  using OpenApiProject1.BusinessLayer;
  using OpenApiProject1.MiddleWareExceptionHandeling;
  using Microsoft.AspNetCore.Hosting; 
  using Microsoft.Extensions.Hosting; 
  using Microsoft.Extensions.Logging;
using OpenApiProject1.Validators;
using OpenApiProject1.SingletonService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); 
//builder.Services.AddTransient(typeof(IDataAccess<>), typeof(IDataAccess<>));
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
  builder.Services.AddTransient<ModelValidators>();
 builder.Services.AddTransient<GlobalExeceptionHandel>();
 builder.Services.AddSingleton<MySingletonService>();
 //Logger part
builder.Logging.ClearProviders(); // clearing exisiting logging provider
builder.Logging.AddConsole();  // add log to the console
builder.Services.AddLogging();  // registering loging service with DI continer
// Read the logging file path from configuration 
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var loggingFilePath = builder.Configuration["Logging:FileLogging:Path"]; 
if (string.IsNullOrEmpty(loggingFilePath)) 
{ 
     new ArgumentNullException(nameof(loggingFilePath), "Logging file path cannot be null or empty.");
      } 
      else{
builder.Logging.AddFile(loggingFilePath);
      }

//builder.Logging.AddFile("D:/VSCode/OpenApi/OpenApiProject1/log/myapp-{Date}.txt");
 //Logger part
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
var logger = app.Services.GetRequiredService<ILogger<Program>>(); //logger
logger.LogInformation("Application has started");  //logger
app.UseHttpsRedirection();
 app.UseAuthorization(); 
 app.MapControllers();

app.UseMiddleware<GlobalExeceptionHandel>();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
