using System.ComponentModel.DataAnnotations;
using System.Transactions;
using BankingAPI.Domain.Exceptions;

namespace BankingAPI.Middlewares;

public class ExceptionHandlingMiddleware
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
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (message, statusCode) = exception switch
        {
            ValidationException ve => ($"Validation error :{ve.Message}", StatusCodes.Status422UnprocessableEntity),
            NotFoundException ne => ($"Not found error :{ne.Message}", StatusCodes.Status404NotFound),
            TransactionException te => ($"Transaction error :{te.Message}", StatusCodes.Status400BadRequest),
            ApiException te => ($"ApiException error :{te.Message}", StatusCodes.Status500InternalServerError),
            _ => ($"Something went wrong :{exception.Message}", StatusCodes.Status500InternalServerError)
        };
        
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ErrorResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        });
    }
}