using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeConnect.Customer.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_useractivity_Colummn_DonationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonationType",
                table: "UserActivities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationType",
                table: "UserActivities");
        }
    }
}
