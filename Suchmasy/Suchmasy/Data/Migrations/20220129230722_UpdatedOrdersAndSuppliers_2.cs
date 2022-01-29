using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suchmasy.Data.Migrations
{
    public partial class UpdatedOrdersAndSuppliers_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SupplierId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SupplierId1",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId1",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId1",
                table: "Orders",
                column: "SupplierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId1",
                table: "Orders",
                column: "SupplierId1",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
