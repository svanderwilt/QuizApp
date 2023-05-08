using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Questions_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Tests_TestId",
                table: "TestQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestQuestion",
                table: "TestQuestion");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestion_QuestionId",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TestQuestion");

            migrationBuilder.RenameColumn(
                name: "TestId",
                table: "TestQuestion",
                newName: "TestID");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "TestQuestion",
                newName: "QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_TestQuestion_TestId",
                table: "TestQuestion",
                newName: "IX_TestQuestion_TestID");

            migrationBuilder.AlterColumn<int>(
                name: "TestID",
                table: "TestQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionID",
                table: "TestQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestQuestion",
                table: "TestQuestion",
                columns: new[] { "QuestionID", "TestID" });

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Questions_QuestionID",
                table: "TestQuestion",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Tests_TestID",
                table: "TestQuestion",
                column: "TestID",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Questions_QuestionID",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_Tests_TestID",
                table: "TestQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestQuestion",
                table: "TestQuestion");

            migrationBuilder.RenameColumn(
                name: "TestID",
                table: "TestQuestion",
                newName: "TestId");

            migrationBuilder.RenameColumn(
                name: "QuestionID",
                table: "TestQuestion",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_TestQuestion_TestID",
                table: "TestQuestion",
                newName: "IX_TestQuestion_TestId");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "TestQuestion",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "TestQuestion",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TestQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestQuestion",
                table: "TestQuestion",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_QuestionId",
                table: "TestQuestion",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Questions_QuestionId",
                table: "TestQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_Tests_TestId",
                table: "TestQuestion",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
