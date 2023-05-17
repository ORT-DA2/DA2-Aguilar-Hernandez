using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    public partial class ArticlesAndCommentsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOffensiveContent",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "HasOffensiveContent",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "OffensiveWords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CommentId",
                table: "OffensiveWords",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OffensiveWords_ArticleId",
                table: "OffensiveWords",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_OffensiveWords_CommentId",
                table: "OffensiveWords",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OffensiveWords_Articles_ArticleId",
                table: "OffensiveWords",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OffensiveWords_Comments_CommentId",
                table: "OffensiveWords",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OffensiveWords_Articles_ArticleId",
                table: "OffensiveWords");

            migrationBuilder.DropForeignKey(
                name: "FK_OffensiveWords_Comments_CommentId",
                table: "OffensiveWords");

            migrationBuilder.DropIndex(
                name: "IX_OffensiveWords_ArticleId",
                table: "OffensiveWords");

            migrationBuilder.DropIndex(
                name: "IX_OffensiveWords_CommentId",
                table: "OffensiveWords");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "OffensiveWords");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "OffensiveWords");

            migrationBuilder.AddColumn<bool>(
                name: "HasOffensiveContent",
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
        }
    }
}
