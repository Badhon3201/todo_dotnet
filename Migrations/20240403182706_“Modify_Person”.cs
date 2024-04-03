using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modify_Person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JoinCount",
                table: "Persons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinCount",
                table: "Persons");
        }
    }
}
