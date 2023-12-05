using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamburgerWebApp.DAL.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extras_Orders_OrderId",
                table: "Extras");

            migrationBuilder.DropIndex(
                name: "IX_Extras_OrderId",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Extras");

            migrationBuilder.CreateTable(
                name: "ExtraOrder",
                columns: table => new
                {
                    ExtrasId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraOrder", x => new { x.ExtrasId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_ExtraOrder_Extras_ExtrasId",
                        column: x => x.ExtrasId,
                        principalTable: "Extras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOrder_OrdersId",
                table: "ExtraOrder",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraOrder");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Extras",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e3df9a99-67c9-4348-bea2-4ba8f817da6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1208d81f-004d-40ab-97e7-3eea9fdb5386");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18abd07e-9484-49ca-8891-70e5444fb3fe", "AQAAAAEAACcQAAAAEFhbg8ynABmruRtPNwHpB6OwUF2etKmOdzDhpiC1B0mRVeyCA3erQnMhivVSTCoL+A==", "9d242fe9-e0aa-4b03-9ef0-a7aa7fc42bf0" });

            migrationBuilder.CreateIndex(
                name: "IX_Extras_OrderId",
                table: "Extras",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extras_Orders_OrderId",
                table: "Extras",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
