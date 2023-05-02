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
            Reply = comment.Reply
        };

        CommentOutModel commentExpected = new CommentOutModel(comment);
        
        var commentLogic = new Mock<ICommentLogic>(MockBehavior.Strict);
        var notificationLogic = new Mock<INotificationLogic>();
        CommentController commentController = new CommentController(commentLogic.Object, notificationLogic.Object);

        commentLogic.Setup(c => c.AddNewComment(It.IsAny<Comment>(), token, article.Object.Id)).Returns(comment);

        var result = commentController.PostNewComment(commentIn,token, article.Object.Id);
        
        commentLogic.VerifyAll();

        var resultObject = result as CreatedResult;
        var userResult = resultObject.Value as CommentOutModel;
        
        Assert.AreEqual(commentExpected.Article, userResult.Article);
        Assert.AreEqual(commentExpected.Body, userResult.Body);
        Assert.AreEqual(commentExpected.Reply,userResult.Reply);
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