using Blog.Domain.Entities;

namespace Blog.IBusinessLogic;

public interface IOffensiveWordLogic
{
    IEnumerable<OffensiveWord> GetAllOffensiveWords();
    OffensiveWord CreateOffensiveWord(string offensiveWord);
    void DeleteOffensiveWord(string offensiveWord);
    bool HasOffensiveWord(string text);
    IEnumerable<OffensiveWord> GetOffensiveWords(string articleContent);
    void ValidateArticleOffensiveWords(Article article);
    void ValidateCommentOffensiveWords(Comment comment);
}