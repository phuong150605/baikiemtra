using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baikiemtra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    ma_hanghoa = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    ten_hanghoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.ma_hanghoa);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");
        }
    }
}
