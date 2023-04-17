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
            Owner = Mock.Of<User>(),
            Article = Mock.Of<Article>(),
            Body = "Buen post Maquinola",
            Reply = "Gracias capo"
        };

        Comment comment = new Comment()
        {
            Owner = Mock.Of<User>(),
            Article = Mock.Of<Article>(),
            Body = "Buen post Maquinola",
            Reply = "Gracias capo"
        };

        CommentOutModel commentExpected = new CommentOutModel(comment);
        
        var commentService = new Mock<ICommentService>(MockBehavior.Strict);

        CommentController commentController = new CommentController(commentService.Object);

        commentService.Setup(c => c.AddNewComment(It.IsAny<Comment>())).Returns(comment);

        var result = commentController.PostNewComment(commentIn);
        
        commentService.VerifyAll();
        var resultObject = result as OkObjectResult;
        var userResult = resultObject.Value as CommentOutModel;
        
        Assert.AreEqual(commentExpected.Id,userResult.Id);
        Assert.AreEqual(commentExpected.Owner, userResult.Owner);
        Assert.AreEqual(commentExpected.Article, userResult.Article);
        Assert.AreEqual(commentExpected.Body, userResult.Body);
        Assert.AreEqual(commentExpected.Reply,userResult.Reply);
    }
    
}