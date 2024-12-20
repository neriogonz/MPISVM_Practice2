using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTeploobmenApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVariantUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Variants",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Variants");
        }
    }
}
