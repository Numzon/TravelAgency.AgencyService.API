using AgencyService.Core.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AgencyService.Adapter.API.Middlewares;
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
	{
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
		try
		{
			await _next(context);
		}
		catch (Exception exception)
		{
			Log.Error("Exception occurred: {Message}", exception.Message);

            if (exception is IExceptionStrategy)
            {
                var strategy = exception as IExceptionStrategy;

                if (strategy == null)
                {
                    throw new InvalidOperationException("Strategy cannot be null");
                }

                await strategy.ModifyAndWriteAsJsonAsync(context.Response);
                return;
            }

            var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server error"
			};

			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(problemDetails);
		}
    }
}
