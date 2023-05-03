create table Notifications
(
    Id             uniqueidentifier not null
        constraint PK_Notifications
            primary key,
    UserToNotifyId uniqueidentifier not null
        constraint FK_Notifications_Users_UserToNotifyId
            references Users,
    CommentId      uniqueidentifier not null
        constraint FK_Notifications_Comments_CommentId
            references Comments,
    IsRead         bit              not null
)
go

create index IX_Notifications_CommentId
    on Notifications (CommentId)
go

create index IX_Notifications_UserToNotifyId
    on Notifications (UserToNotifyId)
go

