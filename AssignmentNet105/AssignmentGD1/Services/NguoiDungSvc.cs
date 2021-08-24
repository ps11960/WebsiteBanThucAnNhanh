using AssignmentGD1.Helpers;
using AssignmentGD1.Models;
using AssignmentGD1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Services
{
    public interface INguoiDungSvc
    {
        List<NguoiDung> GetNguoiDungAll();
        NguoiDung GetNguoiDung(int id);
        int AddNguoiDung(NguoiDung nguoidung);
        int EditNguoiDung(int id, NguoiDung nguoidung);
        public NguoiDung Login(ViewLogin viewLogin);
    }
    public class NguoiDungSvc : INguoiDungSvc
    {
        protected DataContext _context;
        protected IMahoaHelper _mahoaHelper;
        public NguoiDungSvc(DataContext context, IMahoaHelper mahoaHelper)
        {
            _context = context;
            _mahoaHelper = mahoaHelper;
        }

        public List<NguoiDung> GetNguoiDungAll()
        {
            List<NguoiDung> list = new List<NguoiDung>();
            list = _context.NguoiDungs.ToList();
            return list;
        }

        public NguoiDung GetNguoiDung(int id)
        {
            NguoiDung nguoidung = null;
            nguoidung = _context.NguoiDungs.Find(id);
            return nguoidung;
        }

        public int AddNguoiDung(NguoiDung nguoidung)
        {
            int ret = 0;
            try
            {
                nguoidung.Password = _mahoaHelper.Mahoa(nguoidung.Password);
                _context.Add(nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditNguoiDung(int id, NguoiDung nguoidung)
        {
            int ret = 0;
            try
            {
                NguoiDung _nguoidung = null;
                _nguoidung = _context.NguoiDungs.Find(id); //cách này chỉ dùng cho Khóa chính

                _nguoidung.UserName = nguoidung.UserName;
                _nguoidung.FullName = nguoidung.FullName;
                _nguoidung.Title = nguoidung.Title;
                _nguoidung.DOB = nguoidung.DOB;
                _nguoidung.Email = nguoidung.Email;
                _nguoidung.Admin = nguoidung.Admin;
                _nguoidung.Locked = nguoidung.Locked;
                if (nguoidung.Password != null)
                {
                    nguoidung.Password = _mahoaHelper.Mahoa(nguoidung.Password);
                    _nguoidung.Password = nguoidung.Password;
                }
                _context.Update(_nguoidung);
                _context.SaveChanges();
                ret = nguoidung.NguoiDungId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        public NguoiDung Login(ViewLogin viewLogin)
        {
            var u = _context.NguoiDungs.Where(
                p => p.UserName.Equals(viewLogin.UserName)
                && p.Password.Equals(_mahoaHelper.Mahoa(viewLogin.Password))).FirstOrDefault();
            return u;
        }
    }
}

