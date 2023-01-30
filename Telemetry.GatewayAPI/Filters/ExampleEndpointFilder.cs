using System.Reflection.Metadata.Ecma335;

namespace Telemetry.GatewayAPI.Filters
{
	public class ExampleEndpointFilder : IEndpointFilter
	{
		protected readonly ILogger Logger;
		private readonly string _methodName;

		protected ExampleEndpointFilder(ILoggerFactory loggerFactory)
		{
			Logger = loggerFactory.CreateLogger<ExampleEndpointFilder>();
			_methodName = GetType().Name;
		}

		public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			string tid = context.HttpContext.TraceIdentifier;
			Console.WriteLine($"MyEndpointFilter -> Before Next itd: {tid}" );

			bool resultContinue = true;
			if(!resultContinue) 
			{
				return Results.Problem("details", null, 0);
			}

			// Next -> Passthrough to next filter
			var result = await next(context);

			string tid2 = context.HttpContext.TraceIdentifier;
			Console.WriteLine($"MyEndpointFilter -> After Next() tid: {tid2}");

			return result;
		}
	}
}
