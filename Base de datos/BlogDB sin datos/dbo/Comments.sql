create table Comments
(
    Id            uniqueidentifier not null
        constraint PK_Comments
            primary key,
    OwnerId       uniqueidentifier not null
        constraint FK_Comments_Users_OwnerId
            references Users,
    ArticleId     uniqueidentifier not null
        constraint FK_Comments_Articles_ArticleId
            references Articles
            on delete cascade,
    Body          nvarchar(max)    not null,
    DatePublished datetime2        not null,
    Reply         nvarchar(max)
)
go

create index IX_Comments_ArticleId
    on Comments (ArticleId)
go

create index IX_Comments_OwnerId
    on Comments (OwnerId)
go

