using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.DataAccess.Migrations
{
    public partial class uniqueUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");
        }
    }
}
