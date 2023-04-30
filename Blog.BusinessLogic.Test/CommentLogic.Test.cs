using System.Linq.Expressions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class CommentLogicTest
{
    [TestMethod]
    public void AddNewComment()
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Password = "123456",
            Roles = new List<UserRole>{},
            Email = "nicolas@example.com"
        };
        
        var articleTest = new Article()
        {
            Id = Guid.NewGuid(),
            Title = "Test11",
            Content = "Uruguay is a country in south america",
            Owner = user,
            Comments = new List<Comment>(){},
            DateLastModified = DateTime.Now,
            DatePublished = DateTime.Now,
            Image = "image",
            IsPublic = true,
            Template = Template.RectangleTop
            
        };
        var comment = CreateComment();
        var repositoryMock = new Mock<IRepository<Comment>>(MockBehavior.Loose);
        var sessionMock = new Mock<ISessionLogic>();
        var articleMock = new Mock<IArticleLogic>();
        var token = Guid.NewGuid();
        var logic = new CommentLogic(repositoryMock.Object,articleMock.Object, sessionMock.Object);
        articleMock.Setup(a => a.GetArticleById(articleTest.Id)).Returns(articleTest);
        sessionMock.Setup(s => s.GetLoggedUser(token)).Returns(user);
        repositoryMock.Setup(c => c.Insert(It.IsAny<Comment>()));
        repositoryMock.Setup(c => c.Save());
        
        var result = logic.AddNewComment(comment,articleTest.Id,token);
        repositoryMock.VerifyAll();
        
        Assert.AreEqual(comment, result);
    }
    [TestMethod]
    public void DeleteCommentById()
    {
        Comment comment = CreateComment();
        var mockRepository = new Mock<IRepository<Comment>>();
        var sessionLogic = new Mock<ISessionLogic>();
        var articleLogic = new Mock<IArticleLogic>();
        var commentLogic = new CommentLogic(mockRepository.Object, articleLogic.Object, sessionLogic.Object);
        mockRepository.Setup(o => o.GetById(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(comment);
        mockRepository.Setup(o => o.Save());
        
        commentLogic.DeleteCommentById(comment.Id);
        
        mockRepository.VerifyAll();

    }
    
    private static Comment CreateComment()
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