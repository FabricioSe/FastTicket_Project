using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastTicket_Project.Migrations
{
    /// <inheritdoc />
    public partial class addclicksfieldtoevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "Events");
        }
    }
}
