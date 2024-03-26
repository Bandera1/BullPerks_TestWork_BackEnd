using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullPerks_TestWork.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalSupply",
                table: "Tokens",
                type: "real",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<float>(
                name: "CirculatingSupply",
                table: "Tokens",
                type: "real",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {         

            migrationBuilder.AlterColumn<long>(
                name: "TotalSupply",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CirculatingSupply",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);         
        }
    }
}
