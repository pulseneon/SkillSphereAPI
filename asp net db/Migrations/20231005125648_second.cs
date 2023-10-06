using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_net_db.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Students",
                table: "Courses",
                newName: "StudentsIds");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Script",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Script",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "StudentsIds",
                table: "Courses",
                newName: "Students");
        }
    }
}
