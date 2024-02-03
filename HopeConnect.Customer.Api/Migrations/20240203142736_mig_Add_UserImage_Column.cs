using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopeConnect.Customer.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_Add_UserImage_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Users");
        }
    }
}
