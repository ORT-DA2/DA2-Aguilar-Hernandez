using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IBusinessLogic;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class OffensiveWordLogic: IOffensiveWordLogic
{
    private static IRepository<OffensiveWord> _repository;
    private static INotificationStrategy _notificationArticleStrategy;
    private static INotificationStrategy _notificationCommentStrategy;
    private static INotificationLogic _notificationLogic;
    private static IRepository<User> _userRepository;

    public OffensiveWordLogic(IRepository<OffensiveWord> repository, INotificationLogic notificationLogic, IRepository<User> userRepository)
    {
        _repository = repository;
        _notificationLogic = notificationLogic;
        _notificationArticleStrategy = new ArticleNotificationStrategy(userRepository);
        _notificationCommentStrategy = new CommentNotificationStrategy(userRepository);

    }
    
    public IEnumerable<OffensiveWord> GetAllOffensiveWords()
    {
        return _repository.GetAll();
    }

    public OffensiveWord CreateOffensiveWord(string offensiveWord)
    {
        ValidateNull(offensiveWord);
        if(AlreadyExists(offensiveWord))
        {
            throw new ArgumentException("The offensive word already exists");
        }
        
        OffensiveWord word = new OffensiveWord()
        {
            Word = offensiveWord
        };
        
        _repository.Insert(word);
        _repository.Save();

        return word;
    }

    public void DeleteOffensiveWord(string offensiveWord)
    {
        ValidateNull(offensiveWord);
        if (!AlreadyExists(offensiveWord))
        {
            throw new NotFoundException("The offensive word does not exist");
        }
        
        OffensiveWord word = _repository.GetBy(o => o.Word == offensiveWord);
        
        
        _repository.Delete(word);
        _repository.Save();
    }
    
   private bool AlreadyExists(string offensiveWord)
   {
       return _repository.GetAll().Any(o => o.Word == offensiveWord);
   }
   
   private void ValidateNull(string offensiveWord)
   {
       if (offensiveWord == null)
       {
           throw new ArgumentNullException("The offensive word cannot be null");
       }
   }
   
   public bool HasOffensiveWord(string text) {
       IEnumerable<OffensiveWord> offensiveWords = _repository.GetAll();
       return offensiveWords.Any(word => text.ToLower().Contains(word.Word.ToLower()));
   }

   public IEnumerable<OffensiveWord> GetOffensiveWords(string articleContent)
   {
         IEnumerable<OffensiveWord> offensiveWords = _repository.GetAll();
         return offensiveWords.Where(word => articleContent.ToLower().Contains(word.Word.ToLower()));
   }

   public void ValidateArticleOffensiveWords(Article article)
   {
       if(this.HasOffensiveWord(article.Content) || this.HasOffensiveWord(article.Title))
       {
           article.IsPublic = false;
           article.OffensiveContent = this.GetOffensiveWords(article.Content).Concat(this.GetOffensiveWords(article.Title)).ToList();
           _notificationLogic.SendNotification(_notificationArticleStrategy.CreateNotification(article));
           foreach (var notification in _notificationArticleStrategy.CreateAdminNotification(article))
           {
               _notificationLogic.SendNotification(notification);
           }
       }
   }
   
   public void ValidateCommentOffensiveWords(Comment comment)
   {
       List<string> articleOffensiveWords = new();
       if(this.HasOffensiveWord(comment.Body))
       {
           comment.IsPublic = false;
           comment.OffensiveContent = this.GetOffensiveWords(comment.Body).ToList();
           _notificationLogic.SendNotification(_notificationCommentStrategy.CreateNotification(comment));
           foreach (var notification in _notificationCommentStrategy.CreateAdminNotification(comment))
           {
               _notificationLogic.SendNotification(notification);
           }
       }
   }
}