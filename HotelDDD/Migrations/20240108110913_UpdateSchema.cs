using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("bd98b734-c46e-45be-ab21-2606da92b9dd"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("cd6181dd-dc43-4865-b84d-a141251df6ec"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("cec37761-7d07-4f02-9c05-0ad460a4d10a"));

            migrationBuilder.AlterColumn<string>(
                name: "PreferredCurrency",
                table: "Wallet",
                type: "nvarchar(24)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "PreferredCurrency",
                table: "Wallet",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)");

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("bd98b734-c46e-45be-ab21-2606da92b9dd"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, 2 },
                    { new Guid("cd6181dd-dc43-4865-b84d-a141251df6ec"), "Lit 1 place,Wifi,TV", 50m, 0 },
                    { new Guid("cec37761-7d07-4f02-9c05-0ad460a4d10a"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, 1 }
                });
        }
    }
}
