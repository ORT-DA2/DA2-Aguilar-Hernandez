using System.Security.Authentication;
using Blog.Domain.Exceptions;
using Blog.Filters;
using Blog.Models.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Blog.Tests.FiltersTests;

[TestClass]
public class ExceptionFilterTests
{
    private ExceptionFilter _exceptionFilter;
    private Mock<ExceptionContext> _mock;

    [TestInitialize]
    public void SetUp()
    {
        _exceptionFilter = new ExceptionFilter();
        ActionContext actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };

        _mock = new Mock<ExceptionContext>(actionContext, new List<IFilterMetadata>());
    }

    [TestMethod]
    public void NotFoundExceptionReturns404()
    {
        ErrorDto error = new ErrorDto()
        {
            ErrorMessage = "Not found",
            Code = 404
        };
        _mock.SetupAllProperties();
        _mock.Setup(c => c.Exception).Returns(new NotFoundException(error.ErrorMessage));
        
        _exceptionFilter.OnException(_mock.Object);
        var result = _mock.Object.Result as ObjectResult;
        var resultValue = result.Value as ErrorDto;
        
        Assert.AreEqual(resultValue.Code, error.Code);
        Assert.AreEqual(resultValue.ErrorMessage, error.ErrorMessage);
    }
    
    [TestMethod]
    public void InvalidCredentialReturns401()
    {
        ErrorDto error = new ErrorDto()
        {
            ErrorMessage = "Invalid Credentials",
            Code = 401
        };
        _mock.SetupAllProperties();
        _mock.Setup(c => c.Exception).Returns(new InvalidCredentialException(error.ErrorMessage));
        
        _exceptionFilter.OnException(_mock.Object);
        var result = _mock.Object.Result as ObjectResult;
        var resultValue = result.Value as ErrorDto;
        
        Assert.AreEqual(resultValue.Code, error.Code);
        Assert.AreEqual(resultValue.ErrorMessage, error.ErrorMessage);
    }
    
    [TestMethod]
    public void ArgumentExceptionReturns400()
    {
        ErrorDto error = new ErrorDto()
        {
            ErrorMessage = "Bad request",
            Code = 400
        };
        _mock.SetupAllProperties();
        _mock.Setup(c => c.Exception).Returns(new ArgumentException(error.ErrorMessage));
        
        _exceptionFilter.OnException(_mock.Object);
        var result = _mock.Object.Result as ObjectResult;
        var resultValue = result.Value as ErrorDto;
        
        Assert.AreEqual(resultValue.Code, error.Code);
        Assert.AreEqual(resultValue.ErrorMessage, error.ErrorMessage);
    }
    
    [TestMethod]
    public void GenericExceptionReturns500()
    {
        ErrorDto error = new ErrorDto()
        {
            ErrorMessage = "",
            Code = 500
        };
        _mock.SetupAllProperties();
        _mock.Setup(c => c.Exception).Returns(new Exception(error.ErrorMessage));
        
        _exceptionFilter.OnException(_mock.Object);
        var result = _mock.Object.Result as ObjectResult;
        var resultValue = result.Value as ErrorDto;
        
        Assert.AreEqual(resultValue.Code, error.Code);
        Assert.AreEqual(resultValue.ErrorMessage, error.ErrorMessage);
    }

}