using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Migrations
{
    public partial class add_Personal_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persionals_PersionalServices_PersionalServiceId",
                table: "Persionals");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_PersionalServices_PersionalServiceId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "PersionalServices");

            migrationBuilder.DropIndex(
                name: "IX_Services_PersionalServiceId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Persionals_PersionalServiceId",
                table: "Persionals");

            migrationBuilder.DropColumn(
                name: "PersionalServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PersionalServiceId",
                table: "Persionals");

            migrationBuilder.CreateTable(
                name: "PersionalService",
                columns: table => new
                {
                    PersionalsPersionalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServicesServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersionalService", x => new { x.PersionalsPersionalId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_PersionalService_Persionals_PersionalsPersionalId",
                        column: x => x.PersionalsPersionalId,
                        principalTable: "Persionals",
                        principalColumn: "PersionalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersionalService_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersionalService_ServicesServiceId",
                table: "PersionalService",
                column: "ServicesServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersionalService");

            migrationBuilder.AddColumn<int>(
                name: "PersionalServiceId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersionalServiceId",
                table: "Persionals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersionalServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersionalServices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_PersionalServiceId",
                table: "Services",
                column: "PersionalServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Persionals_PersionalServiceId",
                table: "Persionals",
                column: "PersionalServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persionals_PersionalServices_PersionalServiceId",
                table: "Persionals",
                column: "PersionalServiceId",
                principalTable: "PersionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_PersionalServices_PersionalServiceId",
                table: "Services",
                column: "PersionalServiceId",
                principalTable: "PersionalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
