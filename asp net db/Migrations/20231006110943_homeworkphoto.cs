using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_net_db.Migrations
{
    /// <inheritdoc />
    public partial class homeworkphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Homeworks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Homeworks");
        }
    }
}
