using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherActivityPlanner.Migrations
{
    /// <inheritdoc />
    public partial class InitialSavedActivityPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedActivityPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ObservedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    WindSpeedKmh = table.Column<double>(type: "float", nullable: false),
                    PrecipitationMm = table.Column<double>(type: "float", nullable: false),
                    SuggestionTitle = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SuggestionDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SuggestionSeverity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedActivityPlans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedActivityPlans");
        }
    }
}
