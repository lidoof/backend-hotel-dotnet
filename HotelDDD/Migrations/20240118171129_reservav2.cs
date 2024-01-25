using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class reservav2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ReservationRoom",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRoom", x => new { x.ReservationId, x.RoomType });
                    table.ForeignKey(
                        name: "FK_ReservationRoom_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("7ea19e79-a901-47e5-9779-6e3dc022f98d"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, "Superior" },
                    { new Guid("8e230753-bdb8-4bdc-b8e6-67eda791d03e"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, "Suite" },
                    { new Guid("9bc27c47-44d4-4a98-807f-c647dd29bf68"), "Lit 1 place,Wifi,TV", 50m, "Standard" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationRoom");

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("7ea19e79-a901-47e5-9779-6e3dc022f98d"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("8e230753-bdb8-4bdc-b8e6-67eda791d03e"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("9bc27c47-44d4-4a98-807f-c647dd29bf68"));

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Room",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
