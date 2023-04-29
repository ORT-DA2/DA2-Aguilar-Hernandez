using System.Linq.Expressions;
using Blog.Domain.Entities;
using Blog.IDataAccess;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class Comm
{
    [TestMethod]
    public void AddNewComment()
    {
        Comment comment = CreateComment();
        var mock = new Mock<IRepository<Comment>>(MockBehavior.Strict);
        var logic = new CommentService(mock.Object);
        mock.Setup(c => c.Insert(It.IsAny<Comment>()));
        mock.Setup(c => c.Save());
        var result = logic.AddNewComment(comment);
        mock.VerifyAll();
        Assert.AreEqual(comment, result);
    }
    [TestMethod]
    public void DeleteCommentById()
    {
        Comment comment = CreateComment();
        var mock = new Mock<IRepository<Comment>>();
        var logic = new CommentService(mock.Object);
        mock.Setup(o => o.GetById(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(comment);
        mock.Setup(o => o.Save());
        logic.DeleteCommentById(comment.Id);
        
        mock.VerifyAll();

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