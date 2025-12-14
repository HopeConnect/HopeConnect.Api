using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeConnect.Customer.Api.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Add_RecipientTable_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Recipients");
        }
    }
}
