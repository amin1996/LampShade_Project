using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.Infrastructure.EfCore.Migrations
{
    public partial class inventoryChangedToInventoryOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Inventory_InventoryId",
                table: "Operations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "InventoryOperations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryOperations",
                table: "InventoryOperations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_InventoryOperations_InventoryId",
                table: "Operations",
                column: "InventoryId",
                principalTable: "InventoryOperations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_InventoryOperations_InventoryId",
                table: "Operations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryOperations",
                table: "InventoryOperations");

            migrationBuilder.RenameTable(
                name: "InventoryOperations",
                newName: "Inventory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Inventory_InventoryId",
                table: "Operations",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
