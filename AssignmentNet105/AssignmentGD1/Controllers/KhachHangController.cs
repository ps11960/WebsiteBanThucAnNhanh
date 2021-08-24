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
    public class KhachHangController : BaseController
    {
        private IKhachHangSvc _khachhangsvc;
        public KhachHangController(IKhachHangSvc khachHangSvc)
        {
            _khachhangsvc = khachHangSvc;
        }
        // GET: KhachHangController
        public ActionResult Index()
        {
            return View(_khachhangsvc.GetKhachHangAll());
        }

        // GET: KhachHangController/Details/5
        public ActionResult Details(int id)
        {
            return View(_khachhangsvc.GetKhachHang(id));
        }

        // GET: KhachHangController/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: KhachHangController/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KhachHangController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_khachhangsvc.GetKhachHang(id));
        }

        // POST: KhachHangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, KhachHang khachhang)
        {
            try 
            { 
                _khachhangsvc.EditKhachHang(id, khachhang);
                return RedirectToAction(nameof(Details), new { id = khachhang.KhanhHangId });
            }
            catch
            {
                return View();
            }
        }

        // GET: KhachHangController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: KhachHangController/Delete/5
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
