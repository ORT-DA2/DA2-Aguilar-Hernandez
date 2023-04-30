using Blog.BusinessLogic;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
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
        var token = Guid.NewGuid();
        var articleId = Guid.NewGuid();
        
        Comment comment = CreateComment();
        
        CommentInModel commentIn = new CommentInModel()
        {
            Body = comment.Body,
            Reply = comment.Reply
        };

        CommentOutModel commentExpected = new CommentOutModel(comment);
        
        var commentService = new Mock<ICommentLogic>(MockBehavior.Strict);

        CommentController commentController = new CommentController(commentService.Object);

        commentService.Setup(c => c.AddNewComment(It.IsAny<Comment>(),articleId, token)).Returns(comment);

        var result = commentController.PostNewComment(commentIn, token);
        
        commentService.VerifyAll();
        var resultObject = result as OkObjectResult;
        var userResult = resultObject.Value as CommentOutModel;
        
        Assert.AreEqual(commentExpected.Id,userResult.Id);
        Assert.AreEqual(commentExpected.Article, userResult.Article);
        Assert.AreEqual(commentExpected.Body, userResult.Body);
        Assert.AreEqual(commentExpected.Reply,userResult.Reply);
    }

    [TestMethod]
    public void DeleteValidCommentById()
    {
        Comment comment = new Comment();

        var mock = new Mock<ICommentLogic>(MockBehavior.Strict);

        var controller = new CommentController(mock.Object);
        mock.Setup(c => c.DeleteCommentById(comment.Id));
        var result = controller.DeleteCommentById(comment.Id);

        var okResult = result as OkObjectResult;
        mock.VerifyAll();
        Assert.AreEqual(okResult.Value, $"Comment with the id {comment.Id} was deleted");
    }

    [TestMethod]
    public void DeleteInvalidCommentById()
    {
        Comment comment = CreateComment();
        var mock = new Mock<ICommentLogic>(MockBehavior.Strict);
        var controller = new CommentController(mock.Object);
        mock.Setup(c => c.DeleteCommentById(comment.Id)).Throws(new NotFoundException("There are no comments."));
        var result = controller.DeleteCommentById(comment.Id);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
    private Comment CreateComment()
    {
        return new Comment()
        {
            Id = Guid.NewGuid(),
            Owner = Mock.Of<User>(),
            Article = Mock.Of<Article>(),
            Body = "Buen post Maquinola",
            Reply = "Gracias capo"
        };
    }
}