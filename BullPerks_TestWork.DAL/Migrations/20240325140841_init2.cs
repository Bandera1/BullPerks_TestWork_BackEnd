using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullPerks_TestWork.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbdab558-ba78-4b17-898a-ecb54bc42668");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7f8d56df-75c2-4d51-8a6d-d51a26a0b0e1", "55a74d6e-2fae-47b8-8acb-2878317c1a81", "admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f8d56df-75c2-4d51-8a6d-d51a26a0b0e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dbdab558-ba78-4b17-898a-ecb54bc42668", "01426c85-2e1d-4c19-ba52-9ef025b9987c", "admin", null });
        }
    }
}
