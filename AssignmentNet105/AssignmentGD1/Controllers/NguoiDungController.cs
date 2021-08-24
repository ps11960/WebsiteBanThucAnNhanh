using AssignmentGD1.Models;
using AssignmentGD1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Controllers
{
    public class NguoiDungController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private INguoiDungSvc _nguoiDungSvc;
        public NguoiDungController(IWebHostEnvironment webHostEnvironment, INguoiDungSvc nguoiDungSvc)
        {
            _webHostEnviroment = webHostEnvironment;
            _nguoiDungSvc = nguoiDungSvc;
        }
        // GET: NguoiDungController
        public ActionResult Index()
        {
            return View(_nguoiDungSvc.GetNguoiDungAll());
        }

        // GET: NguoiDungController/Details/5
        public ActionResult Details(int id)
        {
            var nguoidung = _nguoiDungSvc.GetNguoiDung(id);
            return View(nguoidung);
        }

        // GET: NguoiDungController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NguoiDungController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NguoiDung nguoidung)
        {
            try
            {
                _nguoiDungSvc.AddNguoiDung(nguoidung);
                return RedirectToAction(nameof(Index), new { id = nguoidung.NguoiDungId });
            }
            catch
            {
                return View();
            }
        }

        // GET: NguoiDungController/Edit/5
        public ActionResult Edit(int id)
        {
            var nguoidung = _nguoiDungSvc.GetNguoiDung(id);
            return View(nguoidung);
        }

        // POST: NguoiDungController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NguoiDung nguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _nguoiDungSvc.EditNguoiDung(id, nguoiDung);
                }
                return RedirectToAction(nameof(Index), new { id = nguoiDung.NguoiDungId });
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NguoiDungController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NguoiDungController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
