﻿using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError($"Error Message: {exception.Message}, Time of occurrence {DateTime.UtcNow}");

        (var detail, var title, var statusCode) = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name,
                StatusCodes.Status500InternalServerError),
            ValidationException => (exception.Message, exception.GetType().Name,
                StatusCodes.Status400BadRequest),
            BadRequestException => (exception.Message, exception.GetType().Name,
                StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name,
                StatusCodes.Status404NotFound),
            _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Detail = detail,
            Title = title,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        
        if(exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}