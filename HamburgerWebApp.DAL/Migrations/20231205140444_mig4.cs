using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerWebApp.DAL.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "58ff1583-b9f7-4567-9e57-9528099a13de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "51fb6a16-a142-4fd0-a251-8dc222dcb532");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8936b128-75db-4d17-90f4-cad439b5a4eb", "AQAAAAEAACcQAAAAEE53rvctM79DHtSLRGRsfKTxr7S78hIyykGwSL4E95Xy0ucmEmd56SPBuU/2lE/xIQ==", "5975285d-adbe-4a7f-88d5-8c85c3c0ccc9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
