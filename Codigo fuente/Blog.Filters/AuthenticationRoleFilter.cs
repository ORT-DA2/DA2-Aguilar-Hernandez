using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Blog.Filters;

public class AuthenticationRoleFilter: Attribute, IActionFilter
{
    private ISessionLogic _sessionLogic;
    public Role[] Roles { get; set; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        this._sessionLogic = context.HttpContext.RequestServices.GetService<ISessionLogic>();
        StringValues token;
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
        ErrorDto error = new ErrorDto()
        {
            ErrorMessage = "User not allowed to do this action",
            Code = 401
        };
        try
        {
            Guid guidToken = new Guid(token);
            User user = this._sessionLogic.GetLoggedUser(guidToken);
            bool roles = this.Roles.Any(role => user.IsInRole(role));
            if (user == null || !roles)
            {
                context.Result = new ObjectResult(error)
                {
                    StatusCode = error.Code
                };
            }
        }
        catch (KeyNotFoundException e)
        {
            context.Result = new ObjectResult(error)
            {
                StatusCode = error.Code
            };
        }
    }
    
    public void OnActionExecuted(ActionExecutedContext context)
    {
       
    }
}