using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class MiseAJourDuModeleCustomerAjoutdeWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("123079df-8799-46ee-8969-c505be3dfd3a"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("6b0cd6e6-fec3-4cae-9c23-01b22ce0ee0f"));

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "Id",
                keyValue: new Guid("db15f27a-dffe-4ef8-ad79-5bd3e0c0e711"));

            migrationBuilder.AddColumn<string>(
                name: "MotDePasse",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreferredCurrency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wallet");

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

            migrationBuilder.DropColumn(
                name: "MotDePasse",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Customer");

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Amenities", "PricePerNight", "Type" },
                values: new object[,]
                {
                    { new Guid("123079df-8799-46ee-8969-c505be3dfd3a"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur", 100m, 1 },
                    { new Guid("6b0cd6e6-fec3-4cae-9c23-01b22ce0ee0f"), "Lit 2 places,Wifi,TV écran plat,Minibar,Climatiseur,Baignoire,Terrasse", 200m, 2 },
                    { new Guid("db15f27a-dffe-4ef8-ad79-5bd3e0c0e711"), "Lit 1 place,Wifi,TV", 50m, 0 }
                });
        }
    }
}
