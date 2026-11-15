using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabEquipmentApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentItems",
                table: "EquipmentItems");

            migrationBuilder.RenameTable(
                name: "EquipmentItems",
                newName: "Equipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment");

            migrationBuilder.RenameTable(
                name: "Equipment",
                newName: "EquipmentItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentItems",
                table: "EquipmentItems",
                column: "Id");
        }
    }
}
