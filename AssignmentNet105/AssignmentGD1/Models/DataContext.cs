using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DonhangChitiet> DonhangChitiets { get; set; }
    }
}
