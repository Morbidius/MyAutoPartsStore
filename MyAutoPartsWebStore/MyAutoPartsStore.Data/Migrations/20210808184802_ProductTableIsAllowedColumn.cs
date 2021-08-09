namespace MyAutoPartsStore.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ProductTableIsAllowedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowed",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowed",
                table: "Products");
        }
    }
}
