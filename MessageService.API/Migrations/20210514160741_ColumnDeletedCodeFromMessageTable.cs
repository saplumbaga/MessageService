using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageService.API.Migrations
{
    public partial class ColumnDeletedCodeFromMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Messages",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
