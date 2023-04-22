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