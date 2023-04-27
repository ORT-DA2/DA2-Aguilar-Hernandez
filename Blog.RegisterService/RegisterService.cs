﻿using Blog.BusinessLogic;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Filters;
using Blog.IBusinessLogic;
using Blog.IDataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.RegisterService;

public class RegisterService
{
    public void ServiceRegistrator(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AuthorizationFilter>();
        serviceCollection.AddScoped<AuthenticationRoleFilter>();
        serviceCollection.AddDbContext<BlogDbContext>();
        serviceCollection.AddScoped<IUserLogic, UserLogic>();
        serviceCollection.AddScoped<IRepository<User>, UserRepository>();
        serviceCollection.AddScoped<ISessionLogic, SessionLogic>();
        serviceCollection.AddScoped<IRepository<Session>, SessionRepository>();
        serviceCollection.AddScoped<ICommentLogic, CommentLogic>();
        serviceCollection.AddScoped<IArticleLogic, ArticleLogic>();
        serviceCollection.AddScoped<IRepository<Article>, ArticleRepository>();

    }
}