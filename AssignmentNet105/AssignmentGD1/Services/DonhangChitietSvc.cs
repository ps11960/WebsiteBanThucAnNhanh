using AssignmentGD1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Services
{
    public interface IDonhangChitietSvc
    {
        int AddDonhangChitietSvc(DonhangChitiet donangchitiet);
    }
    public class DonhangChitietSvc : IDonhangChitietSvc
    {
        protected DataContext _context;
        public DonhangChitietSvc(DataContext context)
        {
            _context = context;
        }
        public int AddDonhangChitietSvc (DonhangChitiet donhangchitiet)
        {
            int ret = 0;
            try
            {
                _context.Add(donhangchitiet);
                _context.SaveChanges();
                ret = donhangchitiet.ChiTietId;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
    }
}
