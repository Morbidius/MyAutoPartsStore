using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAutoPartsStore.Data.Migrations
{
    public partial class AddedSizeCapacityProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeCapacity",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeCapacity",
                table: "Products");
        }
    }
}
