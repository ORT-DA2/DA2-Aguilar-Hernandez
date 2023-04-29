﻿using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Blog.IDataAccess;

namespace Blog.BusinessLogic;

public class CommentService : ICommentService
{
    private readonly IRepository<Comment> _repository;

    public CommentService(IRepository<Comment> repository)
    {
        _repository = repository;
    }


    public Comment AddNewComment(Comment comment)
    {
        _repository.Insert(comment);
        _repository.Save();
        return comment;
    }

    public void DeleteCommentById(Guid id)
    {
        Comment comment = _repository.GetById(c => c.Id == id);
        
        if (comment == null)
        {
            throw new NotFoundException("The comment was not found");
        }
        
        _repository.Delete(comment);
        _repository.Save();
    }
}