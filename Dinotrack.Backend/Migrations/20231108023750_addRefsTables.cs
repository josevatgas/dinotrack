using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dinotrack.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addRefsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Refs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Model",
                table: "Refs",
                type: "int",
                maxLength: 4,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RefImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefImages_Refs_RefId",
                        column: x => x.RefId,
                        principalTable: "Refs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefImages_RefId",
                table: "RefImages",
                column: "RefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Refs");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Refs");
        }
    }
}