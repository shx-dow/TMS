using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TMS.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Smith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AppUserId",
                table: "Tasks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AppUsers_AppUserId",
                table: "Tasks",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AppUsers_AppUserId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AppUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Tasks");
        }
    }
}
