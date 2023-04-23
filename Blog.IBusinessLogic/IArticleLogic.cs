using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IArticleLogic
{
    Article GetArticleById(Guid articleTestId);
}