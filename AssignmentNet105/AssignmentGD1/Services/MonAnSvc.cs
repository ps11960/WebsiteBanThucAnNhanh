using AssignmentGD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Services
{
    public interface IMonAnSvc
    {
        List<MonAn> GetMonAnAll();
        MonAn GetMonAn(int id);
        int AddMonAn(MonAn monAn);
        int EditMonAn(int id, MonAn monAn);
    }
    public class MonAnSvc : IMonAnSvc
    {
        protected DataContext _context;
        public MonAnSvc(DataContext context)
        {
            _context = context;
        }
        public List<MonAn> GetMonAnAll()
        {
            List<MonAn> list = new List<MonAn>();
            list = _context.MonAns.ToList();
            return list;
        }
        public MonAn GetMonAn(int id)
        {
            MonAn monan = null;
            monan = _context.MonAns.Find(id);
            return monan;
        }
        public int AddMonAn(MonAn monan)
        {
            int ret = 0;
            try
            {
                _context.Add(monan);
                _context.SaveChanges();
                ret = monan.MonAnId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public int EditMonAn(int id, MonAn monan)
        {
            int ret = 0;
            try
            {
                MonAn _monan = null;
                _monan = _context.MonAns.Find(id);

                _monan.Name = monan.Name;
                _monan.Gia = monan.Gia;
                _monan.PhanLoai = monan.PhanLoai;
                _monan.HinhAnh = monan.HinhAnh;
                _monan.MoTa = monan.MoTa;
                _monan.TrangThai = monan.TrangThai;

                _context.Update(_monan);
                _context.SaveChanges();
                ret = monan.MonAnId;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
