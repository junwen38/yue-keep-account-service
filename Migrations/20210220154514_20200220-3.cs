using Microsoft.EntityFrameworkCore.Migrations;

namespace YueKeepAccountService.Migrations
{
    public partial class _202002203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_Category2Id",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "Category2Id",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_Category2Id",
                table: "Items",
                column: "Category2Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_Category2Id",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "Category2Id",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_Category2Id",
                table: "Items",
                column: "Category2Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
