using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommercialName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ScientificName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Laboratory = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    IdMedication = table.Column<int>(type: "int", nullable: false),
                    AppoimentId = table.Column<int>(type: "int", nullable: true),
                    IdAppoiment = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalOrders_Appoiments_AppoimentId",
                        column: x => x.AppoimentId,
                        principalTable: "Appoiments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MedicalOrders_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrders_AppoimentId",
                table: "MedicalOrders",
                column: "AppoimentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalOrders_MedicationId",
                table: "MedicalOrders",
                column: "MedicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalOrders");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Medications");
        }
    }
}
