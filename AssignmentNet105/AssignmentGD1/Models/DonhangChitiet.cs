using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Models
{
    public class DonhangChitiet
    {
        [Key]
        public int ChiTietId { get; set; }
        [ForeignKey("DonHang")]
        public int DonHangId { get; set; }
        [ForeignKey("MonAn")]
        public int MonAnId { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "Vui lòng nhập số lượng")]
        [Display(Name = "Số lượng")]
        public int Soluong { get; set; }
        [Required, Range(0, double.MaxValue, ErrorMessage = "Vui lòng nhập thành tiền")]
        [Display(Name = "Thành Tiền")]
        public double Thanhtien { get; set; }
        [StringLength(250)]
        [Display(Name ="Ghi chú")]
        public string Ghichu { get; set; }
        public DonHang DonHang { get; set; }
        public MonAn MonAn { get; set; }
    }
}
