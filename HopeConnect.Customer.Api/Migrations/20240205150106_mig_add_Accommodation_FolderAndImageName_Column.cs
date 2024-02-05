using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeConnect.Customer.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_Accommodation_FolderAndImageName_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Accommodations",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Accommodations");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Accommodations",
                newName: "ImageUrl");
        }
    }
}
