using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerWebApp.DAL.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d191f508-fefe-461e-b4f2-081ca2633cfb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "902497e7-5c0f-4eb2-b583-8696911af5de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7998b03-c862-4d25-b4e8-d2cdda4ec145", "AQAAAAEAACcQAAAAEMh+okdfhm0MEmq4VRhjfcz66WwkavU/vjNGuIrwNfwv2j5vACqf2dBEiaPqSaO+gA==", "ea73aa7c-af3c-46a5-823b-b371540388c0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "cc82a18d-83a7-45ff-9240-50fe99d3a9c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "321c1838-78cc-4243-808e-6b673d41d998");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f56e18b5-af42-42d0-9c14-17c536fc3080", "AQAAAAEAACcQAAAAEGTZcHhhyxWwa4OEdtKJoJYbY6JQlwU3+aSSPf2mhwRgNImEwZqvhIClWIqZAa6xgg==", "62a1fe7d-1bc3-4c2e-af69-56b570b8fd4d" });
        }
    }
}
