using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class rese333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "Deposit",
                table: "Reservation",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("79c43e6b-3c52-4ad1-9d40-908d74249162"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, "Superior" },
                    { new Guid("bde1712c-fba8-4671-b01b-9e32bc9e0b95"), "Lit 1 place,Wifi,TV", 50m, "Standard" },
                    { new Guid("f7233423-9a0d-4dd2-bd05-efdac04694b9"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, "Suite" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("79c43e6b-3c52-4ad1-9d40-908d74249162"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("bde1712c-fba8-4671-b01b-9e32bc9e0b95"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("f7233423-9a0d-4dd2-bd05-efdac04694b9"));

            migrationBuilder.DropColumn(
                name: "Deposit",
                table: "Reservation");

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
    }
}
