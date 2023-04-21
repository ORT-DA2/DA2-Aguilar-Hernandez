using Blog.BusinessLogic;
using Blog.BusinessLogic.Filters;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.RegisterService;

public class RegisterService
{
    public void ServiceRegistrator(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AuthorizationFilter>();
        serviceCollection.AddDbContext<BlogDbContext>();
        serviceCollection.AddScoped<IUserLogic, UserLogic>();
        serviceCollection.AddScoped<IRepository<User>, UserRepository>();
        serviceCollection.AddScoped<ISessionLogic, SessionLogic>();
        serviceCollection.AddScoped<IRepository<Session>, SessionRepository>();
    }
}