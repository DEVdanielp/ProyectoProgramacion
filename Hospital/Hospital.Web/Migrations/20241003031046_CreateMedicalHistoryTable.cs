using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateMedicalHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamePatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppoimentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistory_Appoiments_AppoimentId",
                        column: x => x.AppoimentId,
                        principalTable: "Appoiments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_AppoimentId",
                table: "MedicalHistory",
                column: "AppoimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalHistory");
        }
    }
}
