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
        Comment comment = CreateComment();
        
        CommentInModel commentIn = new CommentInModel()
        {
            Owner = comment.Owner,
            Article = comment.Article,
            Body = comment.Body,
            Reply = comment.Reply
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

    [TestMethod]
    public void DeleteCommentById()
    {
        Comment comment = new Comment();

        var mock = new Mock<ICommentService>(MockBehavior.Strict);

        var controller = new CommentController(mock.Object);
        mock.Setup(c => c.DeleteCommentById(comment.Id));
        var result = controller.DeleteCommentById(comment.Id);

        var okResult = result as OkObjectResult;
        mock.VerifyAll();
        Assert.AreEqual(okResult.Value, $"Comment with the id {comment.Id} was deleted");
    }
    
    private Comment CreateComment()
    {
        return new Comment()
        {
            Owner = Mock.Of<User>(),
            Article = Mock.Of<Article>(),
            Body = "Buen post Maquinola",
            Reply = "Gracias capo"
        };
    }
}