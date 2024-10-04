using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Web.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationRolesPermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesPermisos",
                columns: table => new
                {
                    rolId = table.Column<int>(type: "int", nullable: false),
                    PermisosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermisos", x => new { x.PermisosId, x.rolId });
                    table.ForeignKey(
                        name: "FK_RolesPermisos_Permissions_PermisosId",
                        column: x => x.PermisosId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesPermisos_Roles_rolId",
                        column: x => x.rolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermisos_rolId",
                table: "RolesPermisos",
                column: "rolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesPermisos");
        }
    }
}
