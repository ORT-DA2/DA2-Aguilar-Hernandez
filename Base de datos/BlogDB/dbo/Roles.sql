create table Roles
(
    UserId uniqueidentifier not null
        constraint FK_Roles_Users_UserId
            references Users
            on delete cascade,
    Role   nvarchar(450)    not null,
    constraint PK_Roles
        primary key (UserId, Role)
)
go

