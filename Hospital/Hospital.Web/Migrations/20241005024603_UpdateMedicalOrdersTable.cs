using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicalOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalOrders_Appoiments_AppoimentId",
                table: "MedicalOrders");

            migrationBuilder.DropColumn(
                name: "IdAppoiment",
                table: "MedicalOrders");

            migrationBuilder.DropColumn(
                name: "IdMedication",
                table: "MedicalOrders");

            migrationBuilder.AlterColumn<int>(
                name: "AppoimentId",
                table: "MedicalOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalOrders_Appoiments_AppoimentId",
                table: "MedicalOrders",
                column: "AppoimentId",
                principalTable: "Appoiments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalOrders_Appoiments_AppoimentId",
                table: "MedicalOrders");

            migrationBuilder.AlterColumn<int>(
                name: "AppoimentId",
                table: "MedicalOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdAppoiment",
                table: "MedicalOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMedication",
                table: "MedicalOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalOrders_Appoiments_AppoimentId",
                table: "MedicalOrders",
                column: "AppoimentId",
                principalTable: "Appoiments",
                principalColumn: "Id");
        }
    }
}
