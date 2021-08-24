using AssignmentGD1.Helpers;
using AssignmentGD1.Models;
using AssignmentGD1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Services
{
    public interface IKhachHangSvc
    {
        List<KhachHang> GetKhachHangAll();
        KhachHang GetKhachHang(int id);
        int AddKhachHang(KhachHang khachhang);
        int EditKhachHang(int id, KhachHang khachhang);
        KhachHang Login(ViewWebLogin viewWebLogin);
    }
    public class KhachHangSvc : IKhachHangSvc
    {
        protected DataContext _context;
        protected IMahoaHelper _mahoaHelper;
        public KhachHangSvc(DataContext context, IMahoaHelper mahoaHelper)
        {
            _context = context;
            _mahoaHelper = mahoaHelper;
        }
        public List<KhachHang> GetKhachHangAll()
        {
            List<KhachHang> list = new List<KhachHang>();
            list = _context.KhachHangs.ToList();
            return list;
        }
        public KhachHang GetKhachHang(int id)
        {
            KhachHang khachhang = null;
            khachhang = _context.KhachHangs.Find(id);
            return khachhang;
        }
        public int AddKhachHang(KhachHang khachhang)
        {
            int ret = 0;
            try
            {
                khachhang.Password = _mahoaHelper.Mahoa(khachhang.Password);
                khachhang.ConfirmPassword = khachhang.Password;

                _context.Add(khachhang);
                _context.SaveChanges();
                ret = khachhang.KhanhHangId;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
        public int EditKhachHang(int id, KhachHang khachhang)
        {
            int ret = 0;
            try
            {
                KhachHang _khachhang = null;
                _khachhang = _context.KhachHangs.Find(id);

                _khachhang.FullName = khachhang.FullName;
                _khachhang.NgaySinh = khachhang.NgaySinh;
                _khachhang.PhoneNumber = khachhang.PhoneNumber;
                _khachhang.Email = khachhang.Email;
                if(_khachhang.Password != null)
                {
                    khachhang.Password = _mahoaHelper.Mahoa(khachhang.Password);
                    _khachhang.Password = khachhang.Password;
                    _khachhang.ConfirmPassword = khachhang.Password;
                }
                _khachhang.Mota = khachhang.Mota;

                _context.Update(_khachhang);
                _context.SaveChanges();
                ret = _khachhang.KhanhHangId;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
        public KhachHang Login(ViewWebLogin viewWebLogin)
        {
            var u = _context.KhachHangs.Where(
                p => p.Email.Equals(viewWebLogin.Email)
                && p.Password.Equals(_mahoaHelper.Mahoa(viewWebLogin.Password))
                ).FirstOrDefault();
            return u;
        }
    }
}
