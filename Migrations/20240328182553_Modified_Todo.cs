using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modified_Todo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Persons_PersonId",
                table: "Todos");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Todos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Persons_PersonId",
                table: "Todos",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Persons_PersonId",
                table: "Todos");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Todos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Persons_PersonId",
                table: "Todos",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
