using Blog.BusinessLogic;
using Blog.Domain.Entities;
using Blog.WebApi.Controllers;
using Blog.Models.In;
using Blog.Models.Out;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blog.Tests.WebApiTests;

[TestClass]
public class CommentControllerTest
{
    [TestMethod]
    public void AddingNewComment()
    {
        CommentInModel commentIn = new CommentInModel()
        {
            Body = "Buen post"
        };

        Comment comment = new Comment()
        {
            Body = "Buen post"
        };

        CommentOutModel commentExpected = new CommentOutModel(comment);
        
        var commentService = new Mock<ICommentService>();

        CommentController commentController = new CommentController(commentService.Object);

        commentService.Setup(c => c.AddNewComment(It.IsAny<Comment>())).Returns(comment);

        var result = commentController.PostNewComment(commentIn);
        
        commentService.VerifyAll();
        var resultObject = result as OkObjectResult;
        var userResult = resultObject.Value as CommentOutModel;
        
        Assert.AreEqual(commentExpected.Body, userResult.Body);
    }
    
}