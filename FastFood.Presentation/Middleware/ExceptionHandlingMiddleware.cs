using FastFood.Application.Exceptions;
using ApplicationException = FastFood.Application.Exceptions.ApplicationException;

namespace FastFood.Presentation.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{Message}", exception.Message);

            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = GetStatusCode(exception);

        var response = new
        {
            error = new
            {
                title = GetTitle(exception),
                message = exception.Message,
                errors = GetErrors(exception)
            }
        };

        await httpContext.Response.WriteAsJsonAsync(response);
    }

    private static int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IEnumerable<string> GetErrors(Exception exception)
    {
        IEnumerable<string> errors = new List<string>();

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }
}