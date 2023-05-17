using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    public partial class OffensiveNotificationsFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Comments_CommentId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserToNotifyId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Comments_CommentId",
                table: "Notifications",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserToNotifyId",
                table: "Notifications",
                column: "UserToNotifyId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Comments_CommentId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserToNotifyId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Comments_CommentId",
                table: "Notifications",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserToNotifyId",
                table: "Notifications",
                column: "UserToNotifyId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
