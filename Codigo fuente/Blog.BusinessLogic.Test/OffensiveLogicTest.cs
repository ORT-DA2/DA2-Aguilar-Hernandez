using System.Linq.Expressions;
using Blog.Domain.Entities;
using Blog.Domain.Enums;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.AspNetCore.Hosting;
using Moq;

namespace Blog.BusinessLogic.Test;

[TestClass]
public class OffensiveLogicTest
{
    private Mock<IRepository<OffensiveWord>> _offensiveRepoMock;
    private Mock<INotificationLogic> _notiLogicMock;
    private Mock<INotificationStrategy> _notiStratLogicMock;
    private Mock<IRepository<User>> _userRepoMock;
    private List<OffensiveWord> _words;
    private OffensiveWord _offensiveWord;

    [TestInitialize]
    public void Setup()
    {
        _offensiveRepoMock = new Mock<IRepository<OffensiveWord>>(MockBehavior.Strict);
        _notiLogicMock = new Mock<INotificationLogic>(MockBehavior.Strict);
        _notiStratLogicMock = new Mock<INotificationStrategy>(MockBehavior.Strict);
        _userRepoMock = new Mock<IRepository<User>>(MockBehavior.Strict);

        _offensiveWord = new OffensiveWord()
        {
            Id = 1,
            Word = "hola",
            Articles = null,
            Comments = null
        };
        
        _words = new List<OffensiveWord>()
        {
            _offensiveWord
        };

    }

    [TestCleanup]
    public void Cleanup()
    {
        _offensiveRepoMock.VerifyAll();
        _notiLogicMock.VerifyAll();
    }
    
    [TestMethod]
    public void GetAllOffensivesValidTest()
    {
        var logic = new OffensiveWordLogic(_offensiveRepoMock.Object, _notiLogicMock.Object,  _userRepoMock.Object);
        _offensiveRepoMock.Setup(o => o.GetAll()).Returns(_words);
        var result = logic.GetAllOffensiveWords();
        Assert.AreEqual(_words.Count(), result.Count());
    }
    
    [TestMethod]
    public void CreateOffensiveValidTest()
    {
        OffensiveWord word = new OffensiveWord()
        {
            Word = "offensiveWord"
        };
        var logic = new OffensiveWordLogic(_offensiveRepoMock.Object, _notiLogicMock.Object,  _userRepoMock.Object);
        _offensiveRepoMock.Setup(o => o.GetAll()).Returns(_words);
        _offensiveRepoMock.Setup(o => o.Insert(It.IsAny<OffensiveWord>()));
        _offensiveRepoMock.Setup(o => o.Save());
        var result = logic.CreateOffensiveWord("offensiveWord");
        Assert.AreEqual(word.Word, result.Word);
    }
    
    [TestMethod]
    public void DeleteOffensiveWord_ValidOffensiveWord_DeletesWord()
    {

        
        var logic = new OffensiveWordLogic(_offensiveRepoMock.Object, _notiLogicMock.Object,  _userRepoMock.Object);
        
        _offensiveRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<OffensiveWord, bool>>>()))
            .Returns(_offensiveWord);
        _offensiveRepoMock.Setup(o => o.GetAll()).Returns(_words);
        _offensiveRepoMock.Setup(o => o.Delete(It.IsAny<OffensiveWord>()));
        _offensiveRepoMock.Setup(o => o.Save());
        
        logic.DeleteOffensiveWord(_offensiveWord.Word);
        
    }


}
