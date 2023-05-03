create table Articles
(
    Id               uniqueidentifier not null
        constraint PK_Articles
            primary key,
    Title            nvarchar(max)    not null,
    Content          nvarchar(max)    not null,
    IsPublic         bit              not null,
    OwnerId          uniqueidentifier not null
        constraint FK_Articles_Users_OwnerId
            references Users
            on delete cascade,
    DatePublished    datetime2        not null,
    DateLastModified datetime2        not null,
    Image            nvarchar(max)    not null,
    Template         int              not null
)
go

create index IX_Articles_OwnerId
    on Articles (OwnerId)
go

