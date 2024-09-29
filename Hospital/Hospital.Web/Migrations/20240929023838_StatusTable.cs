using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Web.Migrations
{
    /// <inheritdoc />
    public partial class StatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusAppoiment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppoimentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_Appoiments_AppoimentId",
                        column: x => x.AppoimentId,
                        principalTable: "Appoiments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_AppoimentId",
                table: "Status",
                column: "AppoimentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
