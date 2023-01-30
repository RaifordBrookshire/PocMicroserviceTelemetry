
using System;
using Telemetry.GatewayAPI.Endpoints;

namespace Telemetry.GatewayAPI
{


	public class AppContext
	{
		public static IServiceProvider? ServiceProvider { get; set; }
		public ServiceCollection? ServiceCollection { get; set; }
		public static WebApplication? WebApplication { get; set; }
		public static  ILoggerFactory? LoggerFactory { get; set; }
		public IConfiguration? Configuration { get; set; }
		public static ILoggingBuilder? LoggingBuilder { get; set; }
		public static IHostBuilder? Host { get; set; }
		public IWebHostBuilder? WebHost { get; set; }
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add Services to the container.
			builder.Services.AddAuthorization();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			var app = builder.Build();

			// Add Pipeline Middleware
			app.UseSwagger();
			app.UseAuthorization();

			// Map Endpont Routes to the Handlers
			app.MapGet("/weather", new WeatherEndpoint().Handler).WithName("GetWeather").WithOpenApi();


			//app.MapGet("/filter", () =>
			//{
			//	return "Demonstrating multiple filters chained together.";
			//})
			//.AddEndpointFilter(async (endpointFilterInvocationContext, next) =>
			//{
			//	app.Logger.LogInformation("This is the first filter.");
			//	var result = await next(endpointFilterInvocationContext);
			//	return result;
			//})
			//.AddEndpointFilter(async (endpointFilterInvocationContext, next) =>
			//{
			//	app.Logger.LogInformation("This is the second filter.");
			//	var result = await next(endpointFilterInvocationContext);
			//	return result;
			//})
			//.AddEndpointFilter(async (endpointFilterInvocationContext, next) =>
			//{
			//	app.Logger.LogInformation("This is the third context.");
			//	var result = await next(endpointFilterInvocationContext);
			//	return result;
			//});

			// Example Adding Filters to Endpoint
			//app.MapGet("/", () =>
			//{
			//	return "This is a sample text.";
			//})
			//.AddEndpointFilter<MyCustomFilterA>()
			//.AddEndpointFilter<MyCustomFilterB>()
			//.AddEndpointFilter<MyCustomFilterC>();

			//app.MapGet("/weatherforecast", (HttpContext httpContext) =>
			//{
			//	var forecast = Enumerable.Range(1, 5).Select(index =>
			//		new WeatherForecast
			//		{
			//			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			//			TemperatureC = Random.Shared.Next(-20, 55),
			//			Summary = summaries[Random.Shared.Next(summaries.Length)]
			//		})
			//		.ToArray();
			//	return forecast;
			//})
			//.WithName("GetWeatherForecast")
			//.WithOpenApi();

			// Build up the AppContext for use anywhere in the App
			//AppContext.WebApplication = app;
			//AppContext.ServiceProvider = builder.Services.BuildServiceProvider();

			// Dump Diagnostic State Before Running App
			Console.WriteLine($"Application Name: {builder.Environment.ApplicationName}");
			Console.WriteLine($"Environment Name: {builder.Environment.EnvironmentName}");
			Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
			Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");

			app.Run();
		}
	}
}