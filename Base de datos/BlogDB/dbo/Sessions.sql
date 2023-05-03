create table Sessions
(
    Id        uniqueidentifier not null
        constraint PK_Sessions
            primary key,
    AuthToken uniqueidentifier not null,
    UserId    uniqueidentifier not null
        constraint FK_Sessions_Users_UserId
            references Users
            on delete cascade
)
go

create index IX_Sessions_UserId
    on Sessions (UserId)
go

