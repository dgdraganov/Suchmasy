using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suchmasy.Data.Migrations
{
    public partial class UpdatedOrdersAndSuppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasurment",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurment",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PricePreUnit",
                table: "Suppliers",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "PricePreUnit",
                table: "Orders",
                newName: "UnitPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Suppliers",
                newName: "PricePreUnit");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Orders",
                newName: "PricePreUnit");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurment",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
