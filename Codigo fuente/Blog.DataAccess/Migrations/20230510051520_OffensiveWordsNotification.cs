using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    public partial class OffensiveWordsNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CommentId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasOffensiveContent",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOffensiveContent",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ArticleId",
                table: "Notifications",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Articles_ArticleId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ArticleId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "HasOffensiveContent",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HasOffensiveContent",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Articles");

            migrationBuilder.AlterColumn<Guid>(
                name: "CommentId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
