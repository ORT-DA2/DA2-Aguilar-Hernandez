using Blog.BusinessLogic;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
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
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Email = "nicolas@exmaple.com",
            Password = "123456",
            Roles = new List<UserRole>()

        };
        
        var articleTest = new Article()
        {
            Id = Guid.NewGuid(),
            Title = ".NET 6 Webpage",
            Content = "New features about .NET are being developed",
            Owner = user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = "test.jpg",
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        
        var token = Guid.NewGuid();
        var comment = CreateComment();
        
        CommentInModel commentIn = new CommentInModel()
        {
            ArticleId = articleTest.Id,
            Body = comment.Body,
        };

        CommentOutModel commentExpected = new CommentOutModel(comment);
        
        var commentLogic = new Mock<ICommentLogic>(MockBehavior.Strict);
        var notificationLogic = new Mock<INotificationLogic>();
        CommentController commentController = new CommentController(commentLogic.Object, notificationLogic.Object);

        commentLogic.Setup(c => c.AddNewComment(It.IsAny<Comment>(), commentIn.ArticleId, token)).Returns(comment);

        var result = commentController.PostNewComment(commentIn, token);
        
        commentLogic.VerifyAll();

        var resultObject = result as CreatedResult;

        var commentResult = resultObject.Value as CommentOutModel;
        commentLogic.VerifyAll();
        Assert.AreEqual(commentExpected.Article, commentResult.Article);
        Assert.AreEqual(commentExpected.Body, commentResult.Body);

    }

    [TestMethod]
    public void DeleteValidCommentById()
    {
        Comment comment = new Comment();

        var mock = new Mock<ICommentLogic>(MockBehavior.Strict);
        var notificationLogic = new Mock<INotificationLogic>(MockBehavior.Strict);
        var controller = new CommentController(mock.Object, notificationLogic.Object);
        mock.Setup(c => c.DeleteCommentById(comment.Id));
        var result = controller.DeleteCommentById(comment.Id);

        var okResult = result as OkObjectResult;
        mock.VerifyAll();
        Assert.AreEqual(okResult.Value, $"Comment with the id {comment.Id} was deleted");
    }

    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void DeleteInvalidCommentById()
    {
        Comment comment = CreateComment();
        var mock = new Mock<ICommentLogic>(MockBehavior.Strict);
        var notificationLogic = new Mock<INotificationLogic>(MockBehavior.Strict);
        var controller = new CommentController(mock.Object, notificationLogic.Object);
        mock.Setup(c => c.DeleteCommentById(comment.Id)).Throws(new NotFoundException("There are no comments."));
        var result = controller.DeleteCommentById(comment.Id);
        mock.VerifyAll();
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public void ReplyValidComment()
    {
        Comment comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Owner = Mock.Of<User>(),
            Article = Mock.Of<Article>(),
            Body = "Buen post Maquinola"
        };
        ReplyCommentDto reply = new ReplyCommentDto()
        {
            Id = comment.Id,
            Reply = "Gracias!"
        };
        Comment commentReplied = comment;
        commentReplied.Reply = "Muchas gracias!";
        var mock = new Mock<ICommentLogic>(MockBehavior.Strict);
        var notificationLogic = new Mock<INotificationLogic>(MockBehavior.Strict);
        var controller = new CommentController(mock.Object, notificationLogic.Object);
        mock.Setup(c => c.ReplyComment(comment.Id, It.IsAny<string>())).Returns(commentReplied);
        var result = controller.ReplyComment(reply);
        var resultObject = result as OkObjectResult;
        var commentResult = resultObject.Value as CommentOutModel;
        mock.VerifyAll();
        Assert.AreEqual(commentReplied.Body, commentResult.Body);
        Assert.AreEqual(commentReplied.Reply,commentResult.Reply);
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