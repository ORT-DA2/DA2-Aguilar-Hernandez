using Blog.BusinessLogic.Error;
using Blog.IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Blog.Filters;

public class AuthorizationFilter: Attribute, IAuthorizationFilter
{
    private ISessionLogic? _sessionLogic;
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        this._sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();
        ErrorDto errorDto = new ErrorDto()
        {
            ErrorMessage = "Unauthorized",
            Code = 401
        };

        StringValues token;
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
        if (token.Count == 0 || token == "")
        {
            context.Result = new ObjectResult(errorDto)
            {
                StatusCode = errorDto.Code
            };
        }

        try
        {
            Guid guidToken = new Guid(token);
            _sessionLogic.GetLoggedUser(guidToken);
        }
        catch (KeyNotFoundException e)
        {
            context.Result = new ObjectResult(errorDto)
            {
                StatusCode = errorDto.Code
            };
        }
    }
}