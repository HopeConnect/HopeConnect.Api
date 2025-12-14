using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeConnect.Customer.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_UserActivity_HelpTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelpType",
                table: "UserActivities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpType",
                table: "UserActivities");
        }
    }
}
