using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelDDD.Migrations
{
    /// <inheritdoc />
    public partial class RoomMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amenities = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
