using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baikiemtra.Migrations
{
    /// <inheritdoc />
    public partial class AddGhiChuToGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ghi_chu",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ghi_chu",
                table: "Goods");
        }
    }
}
