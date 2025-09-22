using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcProduct.Migrations.MvcFish
{
    /// <inheritdoc />
    public partial class AquaCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fishs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaughtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WaterType = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fishs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fishs");
        }
    }
}
