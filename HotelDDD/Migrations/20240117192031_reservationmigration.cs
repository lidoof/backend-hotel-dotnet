using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class reservationmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("9b99f7d9-a951-4710-ac66-b421ce93484f"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("d6cee7cf-5a30-4677-b9bb-48718da0b959"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("e37e480d-8c2c-4b39-9a16-d2c072065a65"));

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("514bc58c-3ee1-426e-bfe2-20c6df502383"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, 1 },
                    { new Guid("7f0df708-e5de-4407-aa70-06f4b90d6246"), "Lit 1 place,Wifi,TV", 50m, 0 },
                    { new Guid("e346ceb3-5c86-4892-8c57-4458ee3d1ca5"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("514bc58c-3ee1-426e-bfe2-20c6df502383"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("7f0df708-e5de-4407-aa70-06f4b90d6246"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("e346ceb3-5c86-4892-8c57-4458ee3d1ca5"));

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("9b99f7d9-a951-4710-ac66-b421ce93484f"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, 2 },
                    { new Guid("d6cee7cf-5a30-4677-b9bb-48718da0b959"), "Lit 1 place,Wifi,TV", 50m, 0 },
                    { new Guid("e37e480d-8c2c-4b39-9a16-d2c072065a65"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, 1 }
                });
        }
    }
}
