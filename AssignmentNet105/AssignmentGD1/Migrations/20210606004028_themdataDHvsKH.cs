using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentGD1.Migrations
{
    public partial class themdataDHvsKH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhanhHangId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Mota = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhanhHangId);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangId = table.Column<int>(nullable: false),
                    Ngaydat = table.Column<DateTime>(nullable: false),
                    TongTien = table.Column<double>(nullable: false),
                    TrangthaiDonhang = table.Column<int>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangId);
                    table.ForeignKey(
                        name: "FK_DonHangs_KhachHangs_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHangs",
                        principalColumn: "KhanhHangId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonhangChitiets",
                columns: table => new
                {
                    ChiTietId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangId = table.Column<int>(nullable: false),
                    MonAnId = table.Column<int>(nullable: false),
                    Soluong = table.Column<int>(nullable: false),
                    Thanhtien = table.Column<double>(nullable: false),
                    Ghichu = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonhangChitiets", x => x.ChiTietId);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_DonHangs_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonhangChitiets_MonAns_MonAnId",
                        column: x => x.MonAnId,
                        principalTable: "MonAns",
                        principalColumn: "MonAnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_KhachHangId",
                table: "DonHangs",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_DonHangId",
                table: "DonhangChitiets",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonhangChitiets_MonAnId",
                table: "DonhangChitiets",
                column: "MonAnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonhangChitiets");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");
        }
    }
}
