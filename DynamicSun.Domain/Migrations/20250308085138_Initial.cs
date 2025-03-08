using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicSun.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AirHumidity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DewPoint = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Pressure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Wind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: true),
                    Cloudiness = table.Column<int>(type: "int", nullable: true),
                    CloudLimit = table.Column<int>(type: "int", nullable: true),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: true),
                    Appearance = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
