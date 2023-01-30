using System.Text.Json;
using Telemetry.GatewayAPI.Models;

namespace Telemetry.GatewayAPI.Endpoints
{
	public class WeatherEndpoint
	{
		public void Handler2(HttpContext httpContext)
		{
			Endpoint? endpoint = httpContext.GetEndpoint();

			Console.WriteLine("Inside Handler 2"   + endpoint.DisplayName + "   " + httpContext.TraceIdentifier);
		}
		public void Handler(HttpContext httpContext)
		{
			var summaries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};

			var forecast = Enumerable.Range(1, 5).Select(index =>
				new WeatherForecast
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = summaries[Random.Shared.Next(summaries.Length)]
				})
				.ToArray();

			var json = JsonSerializer.Serialize(forecast);
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.WriteAsync(json);
		}
	}
}