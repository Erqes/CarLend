using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarRents_CarRentalid",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_CarRents_CarRentalid",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "CarRentalid",
                table: "Employees",
                newName: "CarRentalId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CarRentalid",
                table: "Employees",
                newName: "IX_Employees_CarRentalId");

            migrationBuilder.RenameColumn(
                name: "CarRentalid",
                table: "Cars",
                newName: "CarRentalId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarRentalid",
                table: "Cars",
                newName: "IX_Cars_CarRentalId");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HorsePower",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cars",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarRents_CarRentalId",
                table: "Cars",
                column: "CarRentalId",
                principalTable: "CarRents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_CarRents_CarRentalId",
                table: "Employees",
                column: "CarRentalId",
                principalTable: "CarRents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarRents_CarRentalId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_CarRents_CarRentalId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "HorsePower",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarRentalId",
                table: "Employees",
                newName: "CarRentalid");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CarRentalId",
                table: "Employees",
                newName: "IX_Employees_CarRentalid");

            migrationBuilder.RenameColumn(
                name: "CarRentalId",
                table: "Cars",
                newName: "CarRentalid");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarRentalId",
                table: "Cars",
                newName: "IX_Cars_CarRentalid");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarRents_CarRentalid",
                table: "Cars",
                column: "CarRentalid",
                principalTable: "CarRents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_CarRents_CarRentalid",
                table: "Employees",
                column: "CarRentalid",
                principalTable: "CarRents",
                principalColumn: "Id");
        }
    }
}
