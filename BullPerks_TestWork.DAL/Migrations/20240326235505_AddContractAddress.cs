using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullPerks_TestWork.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddContractAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {        
            migrationBuilder.AddColumn<string>(
                name: "ContractAddress",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: true);        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.DropColumn(
                name: "ContractAddress",
                table: "Tokens");
        }
    }
}
