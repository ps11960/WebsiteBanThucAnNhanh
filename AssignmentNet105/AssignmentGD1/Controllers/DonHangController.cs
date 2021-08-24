using AssignmentGD1.Models;
using AssignmentGD1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Controllers
{
    public class DonHangController : BaseController
    {
        private IDonHangSvc _donhangSvc;
        public DonHangController(IDonHangSvc donhangSvc)
        {
            _donhangSvc = donhangSvc;
        }
        // GET: DonHangController
        public ActionResult Index()
        {
            return View(_donhangSvc.GetDonHangAll());
        }

        // GET: DonHangController/Details/5
        public ActionResult Details(int id)
        {
            return View(_donhangSvc.GetDonHang(id));
        }

        // GET: DonHangController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DonHangController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: DonHangController/Edit/5
        public ActionResult Edit(int id)
        {
            var donhang = _donhangSvc.GetDonHang(id);
            return View(donhang);
        }

        // POST: DonHangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DonHang donhang)
        {
            try
            {
                donhang.KhachHang = null;
                _donhangSvc.EditDonHang(id, donhang);
                return RedirectToAction(nameof(Details), new { id = donhang.DonHangId});
            }
            catch
            {
                return View();
            }
        }

        // GET: DonHangController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: DonHangController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
