using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentGD1.Migrations
{
    public partial class TaodataMonan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MonAnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Gia = table.Column<decimal>(type: "money", nullable: false),
                    PhanLoai = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MonAnId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonAns");
        }
    }
}
