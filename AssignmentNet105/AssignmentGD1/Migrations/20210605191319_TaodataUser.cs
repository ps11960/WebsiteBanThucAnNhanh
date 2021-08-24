using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentGD1.Migrations
{
    public partial class TaodataUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    NguoiDungId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DOB = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Admin = table.Column<bool>(nullable: false),
                    Locked = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.NguoiDungId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
