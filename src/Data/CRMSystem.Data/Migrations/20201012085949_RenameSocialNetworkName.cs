using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMSystem.Data.Migrations
{
    public partial class RenameSocialNetworkName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "networkTitle",
                table: "SocialNetworks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SocialNetworks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SocialNetworks");

            migrationBuilder.AddColumn<string>(
                name: "networkTitle",
                table: "SocialNetworks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
