﻿using Blog.Domain.Enums;

namespace Blog.Domain.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublic { get; set; }
    public User Owner { get; set; }
    public DateTime DatePublished { get; set; }
    public DateTime DateLastModified { get; set; }
    public List<Comment>? Comments { get; set; }
    public byte[]? Image { get; set; }
    public Template Template { get; set; }
}