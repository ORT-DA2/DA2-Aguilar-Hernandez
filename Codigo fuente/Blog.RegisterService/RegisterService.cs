using Blog.BusinessLogic;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Blog.ImporterInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.RegisterService;

public class RegisterService
{
    public void ServiceRegistrator(IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        
        serviceCollection.AddScoped<AuthorizationFilter>();
        serviceCollection.AddScoped<AuthenticationRoleFilter>();
        serviceCollection.AddDbContext<BlogDbContext>();
        serviceCollection.AddScoped<IUserLogic, UserLogic>();
        serviceCollection.AddScoped<IRepository<User>, UserRepository>();
        serviceCollection.AddScoped<ISessionLogic, SessionLogic>();
        serviceCollection.AddScoped<IRepository<Session>, SessionRepository>();
        serviceCollection.AddScoped<IRepository<Comment>, CommentRepository>();
        serviceCollection.AddScoped<ICommentLogic, CommentLogic>();
        serviceCollection.AddScoped<IArticleLogic, ArticleLogic>();
        serviceCollection.AddScoped<INotificationLogic, NotificationLogic>();
        serviceCollection.AddScoped<IRepository<Article>, ArticleRepository>();
        serviceCollection.AddScoped<IRepository<Notification>, NotificationRepository>();
        serviceCollection.AddScoped<IOffensiveWordLogic, OffensiveWordLogic>();
        serviceCollection.AddScoped<IRepository<OffensiveWord>, Repository<OffensiveWord>>();
        serviceCollection.AddScoped<INotificationStrategy, ArticleNotificationStrategy>();
        serviceCollection.AddScoped<INotificationStrategy, CommentNotificationStrategy>();
        serviceCollection.AddScoped<IImporterLogic, ImporterLogic>();
        serviceCollection.AddScoped<IImporter, JsonImporter.JsonImporter>();

    }
}