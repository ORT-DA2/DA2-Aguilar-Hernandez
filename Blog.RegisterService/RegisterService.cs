using Blog.BusinessLogic;
using Blog.DataAccess;

using Microsoft.Extensions.DependencyInjection;

namespace Blog.RegisterService;

public class RegisterService
{
    public void ServiceRegistrator(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<BlogDbContext>();
        serviceCollection.AddScoped<IUserLogic, UserLogic>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }
}