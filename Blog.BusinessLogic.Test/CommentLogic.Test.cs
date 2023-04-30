using System.Linq.Expressions;
using Blog.Domain.Entities;
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
        Comment comment = CreateComment();
        var repositoryMock = new Mock<IRepository<Comment>>(MockBehavior.Strict);
        var sessionMock = new Mock<ISessionLogic>();
        var articleMock = new Mock<IArticleLogic>();
        var token = Guid.NewGuid();
        var articleId = Guid.NewGuid();
        var logic = new CommentLogic(repositoryMock.Object, sessionMock.Object, articleMock.Object);
        repositoryMock.Setup(c => c.Insert(It.IsAny<Comment>()));
        repositoryMock.Setup(c => c.Save());
        
        var result = logic.AddNewComment(comment,token,articleId);
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
        var commentLogic = new CommentLogic(mockRepository.Object, sessionLogic.Object, articleLogic.Object);
        mockRepository.Setup(o => o.GetById(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(comment);
        mockRepository.Setup(o => o.Save());
        
        commentLogic.DeleteCommentById(comment.Id);
        
        mockRepository.VerifyAll();

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