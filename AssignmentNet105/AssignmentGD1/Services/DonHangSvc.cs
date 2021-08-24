using AssignmentGD1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Services
{
    public interface IDonHangSvc
    {
        List<DonHang> GetDonHangAll();
        List<DonHang> GetDonhangByKhachhang(int khachhangId);
        DonHang GetDonHang(int id);
        int AddDonHang(DonHang donhang);
        int EditDonHang(int id, DonHang donhang);
    }
    public class DonHangSvc : IDonHangSvc
    {
        protected DataContext _datacontext;
        public DonHangSvc(DataContext context)
        {
            _datacontext = context;
        }
        public List<DonHang> GetDonHangAll()
        {
            List<DonHang> list = new List<DonHang>();
            list = _datacontext.DonHangs.OrderByDescending(x => x.Ngaydat)
                .Include(x => x.KhachHang).Include(x => x.DonhangChitiets).ToList();
            return list;
        }
        public List<DonHang> GetDonhangByKhachhang(int khachhangId)
        {
            List<DonHang> list = new List<DonHang>();
            list = _datacontext.DonHangs.Where(x => x.KhachHangId == khachhangId)
                .OrderByDescending(x => x.Ngaydat).Include(x => x.KhachHang)
                .Include(x => x.DonhangChitiets)
                .ToList();
            return list;
        }
        public DonHang GetDonHang(int id)
        {
            DonHang donhang = null;
            donhang = _datacontext.DonHangs.Where(x => x.DonHangId == id)
                .Include(x => x.KhachHang).Include(x => x.DonhangChitiets).ThenInclude(y => y.MonAn)
                .FirstOrDefault();
            return donhang;
        }
        public int AddDonHang(DonHang donhang)
        {
            int ret = 0;
            try
            {
                _datacontext.Add(donhang);
                _datacontext.SaveChanges();
                ret = donhang.DonHangId;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
        public int EditDonHang(int id, DonHang donhang)
        {
            int ret = 0;
            try
            {
                _datacontext.Update(donhang);
                _datacontext.SaveChanges();
                ret = donhang.DonHangId;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
    }
}
