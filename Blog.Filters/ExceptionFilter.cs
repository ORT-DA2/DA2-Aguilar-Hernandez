using System.Security.Authentication;
using Blog.Domain.Exceptions;
using Blog.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Filters;

public class ExceptionFilter:Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        List<Type> errors404 = new List<Type>()
        {
            typeof(NotFoundException)
        };
        List<Type> errors401 = new List<Type>()
        {
            typeof(InvalidCredentialException)
        };
        ErrorDto response = new ErrorDto()
        {
            ErrorMessage = context.Exception.Message
        };
        Type errorType = context.Exception.GetType();
        if (errors401.Contains(errorType))
        {
            response.Code = 401;
        }else if (errors404.Contains(errorType))
        {
            response.Code = 404;
        }
        else
        {
            response.Code = 500;
            Console.Write(context.Exception);
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.Code
        };
    }
}