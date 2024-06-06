using testapiproject.MyLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region log4net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
#endregion


#region defaultlogproviders
//builder.Logging.ClearProviders(); //clears all logging providers
// builder.Logging.AddConsole();
// builder.Logging.AddDebug(); // allows only debug logs 
#endregion

#region Serilog settings
/*
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

//builder.Services.AddSerilog();// - this will override the  built in log provider and allow only the serilog
builder.Logging.AddSerilog();  // this will allow both defaul providers and the serilogs
*/
#endregion


// Add services to the container.
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMyLogger, LogToServerMemory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();



// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
