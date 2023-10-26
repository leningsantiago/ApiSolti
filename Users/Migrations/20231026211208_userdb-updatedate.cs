using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Users.Migrations
{
    /// <inheritdoc />
    public partial class userdbupdatedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11cb9e13-47e2-4d96-9e21-2ff82ddec7ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a655159-564f-44f2-9752-8885d38fc310");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a08d6b99-b332-452a-bb40-cde0b9bbf59e");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateUser",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUser",
                table: "AspNetUsers",
                type: "datetime2",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c17f691-aad1-4c8f-83c7-1c2380305608", null, "Responsible", "RESPONSIBLE" },
                    { "68b2e7a5-ad5e-41c3-8b14-abea17d14c86", null, "User", "USER" },
                    { "ed89c5c1-d375-4180-b3be-8a30251a7fab", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c17f691-aad1-4c8f-83c7-1c2380305608");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68b2e7a5-ad5e-41c3-8b14-abea17d14c86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed89c5c1-d375-4180-b3be-8a30251a7fab");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateUser",
                table: "AspNetUsers",
                type: "datetime2",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedUser",
                table: "AspNetUsers",
                type: "datetime2",
                rowVersion: true,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11cb9e13-47e2-4d96-9e21-2ff82ddec7ce", null, "Responsible", "RESPONSIBLE" },
                    { "1a655159-564f-44f2-9752-8885d38fc310", null, "User", "USER" },
                    { "a08d6b99-b332-452a-bb40-cde0b9bbf59e", null, "Admin", "ADMIN" }
                });
        }
    }
}
