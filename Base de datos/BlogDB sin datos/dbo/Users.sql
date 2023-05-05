create table Users
(
    Id        uniqueidentifier not null
        constraint PK_Users
            primary key,
    FirstName nvarchar(max)    not null,
    LastName  nvarchar(max)    not null,
    Username  nvarchar(12)     not null,
    Password  nvarchar(16)     not null,
    Email     nvarchar(450)    not null
)
go

create unique index IX_Users_Email
    on Users (Email)
go

create unique index IX_Users_Username
    on Users (Username)
go

