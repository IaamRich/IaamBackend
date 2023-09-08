using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class Update_AddressTable_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AddressEntity_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.CreateTable(
                name: "AspNetAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    AdditionalAddress = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAddress", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetAddress_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "AspNetAddress",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetAddress_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetAddress");

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdditionalAddress = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AddressEntity_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "AddressEntity",
                principalColumn: "Id");
        }
    }
}
