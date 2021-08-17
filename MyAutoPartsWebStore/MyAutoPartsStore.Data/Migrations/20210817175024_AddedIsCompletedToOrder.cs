using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAutoPartsStore.Data.Migrations
{
    public partial class AddedIsCompletedToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "OrderProducts");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "OrderProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
