﻿using Blog.Domain.Entities;
using Blog.Domain.Enums;

namespace Blog.Tests.DomainTests;

[TestClass]
public class CommentTest
{
    [TestMethod]
    public void GetAndSetCommentTest()
    {
        DateTime time = new DateTime(DateTime.Now.Hour);
        List<Comment> comments = new List<Comment>();
        string linkImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Angular_full_color_logo.svg/1200px-Angular_full_color_logo.svg.png";
        User user = new User(){
            FirstName = "Nicolas",
            LastName = "Hernandez",
            Username = "NicolasAHF",
            Email = "nicolashernandez@example.com",
            Role = Role.Blogger
        };
        Article article = new Article()
        {
            Owner = user,
            Title = "Learn Angular",
            Content = "Angular is a frontend framework",
            IsPublic = true,
            Image = linkImage,
            DatePublished = time,
            DateLastModified = time,
            Comments = comments
        };
        
        Comment comment = new Comment();
        comment.Id = new Guid();
        Guid id = comment.Id;
        comment.Owner = user;
        comment.Article = article;
        comment.CommentBody = "Nice Article";
        comment.Reply = "Thank you!";

        Assert.AreEqual(id, comment.Id);
        Assert.AreEqual(user, comment.Owner);
        Assert.AreEqual(article, comment.Article);
        Assert.AreEqual("Nice Article", comment.CommentBody);
        Assert.AreEqual("Thank you!", comment.Reply);
    }
}