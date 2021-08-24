using AssignmentGD1.Constant;
using AssignmentGD1.Models;
using AssignmentGD1.Models.ViewModels;
using AssignmentGD1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private INguoiDungSvc _nguoiDungSvc;
        public AdminController(IWebHostEnvironment webHostEnvironment, INguoiDungSvc nguoiDungSvc)
        {
            _webHostEnviroment = webHostEnvironment;
            _nguoiDungSvc = nguoiDungSvc;
        }
        // GET: AdminController
        public IActionResult Login(string returnUrl)
        {
            string username = HttpContext.Session.GetString(SessionKey.NguoiDung.UserName);
            if (username != null && username != "") //đã có session
            {
                return RedirectToAction("Index", "NguoiDung");
            }
            #region Hiển thị login
            ViewLogin login = new ViewLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                NguoiDung nguoidung = _nguoiDungSvc.Login(viewLogin);
                if (nguoidung != null)
                {
                    HttpContext.Session.SetString(SessionKey.NguoiDung.UserName, nguoidung.UserName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.FullName, nguoidung.FullName);
                    HttpContext.Session.SetString(SessionKey.NguoiDung.NguoiDungContext,
                        JsonConvert.SerializeObject(nguoidung));
                    return RedirectToAction(nameof(NguoiDungController.Index), "Home");
                }
            }
            return View(viewLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.NguoiDung.UserName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.FullName);
            HttpContext.Session.Remove(SessionKey.NguoiDung.NguoiDungContext);
            return RedirectToAction(nameof(NguoiDungController.Index), "Home");
        }
    }
}
