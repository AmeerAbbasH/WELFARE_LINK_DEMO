using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelfareLinkPRJ.Migrations
{
    /// <inheritdoc />
    public partial class disbursement_table_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Disbursements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Disbursements");
        }
    }
}
