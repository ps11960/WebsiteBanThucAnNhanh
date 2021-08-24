using AssignmentGD1.Constant;
using AssignmentGD1.Filters;
using AssignmentGD1.Models;
using AssignmentGD1.Models.ViewModels;
using AssignmentGD1.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentGD1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IWebHostEnvironment _webHostEnviroment;
        IMonAnSvc _monAnSvc;
        IKhachHangSvc _khachHangSvc;
        IDonHangSvc _donHangSvc;
        IDonhangChitietSvc _donhangChitietSvc;
        public HomeController(IWebHostEnvironment webHostEnvironment, IMonAnSvc monAnSvc,
            IKhachHangSvc khachHangSvc, IDonHangSvc donHangSvc, IDonhangChitietSvc donhangChitietSvc)
        {
            _webHostEnviroment = webHostEnvironment;
            _monAnSvc = monAnSvc;
            _khachHangSvc = khachHangSvc;
            _donHangSvc = donHangSvc;
            _donhangChitietSvc = donhangChitietSvc;
        }
        public IActionResult Index()
        {
            var listma = _monAnSvc.GetMonAnAll();
            return View(listma);
        }
        public IActionResult AddCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                var monan = _monAnSvc.GetMonAn(id);
                List<ViewCart> listcart = new List<ViewCart>()
                {
                    new ViewCart{
                        MonAn = monan,
                        Quantity = 1
                    }
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listcart));
            }
            else
            {
                List<ViewCart> datacart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                bool check = true;
                for(int i = 0; i < datacart.Count; i++)
                {
                    if(datacart[i].MonAn.MonAnId == id)
                    {
                        datacart[i].Quantity++;
                        check = false;
                    }
                }
                if (check)
                {
                    var monan = _monAnSvc.GetMonAn(id);
                    datacart.Add(new ViewCart
                    {
                        MonAn = monan,
                        Quantity = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(datacart));
            }
            return Ok();
        }
        #region Login - Logout - Register
        public IActionResult Login(string returnUrl)
        {
            string KH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            if(KH_Email != null && KH_Email != "")
            {
                return RedirectToAction("Index", "Home");
            }
            #region Hiển thị Login
            ViewWebLogin login = new ViewWebLogin();
            login.ReturnUrl = returnUrl;
            return View(login);
            #endregion
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewWebLogin viewWebLogin)
        {
            if (ModelState.IsValid)
            {
                KhachHang khachHang = _khachHangSvc.Login(viewWebLogin);
                if(khachHang != null)
                {
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_Email, khachHang.Email);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KH_FullName, khachHang.FullName);
                    HttpContext.Session.SetString(SessionKey.KhachHang.KhachHangContext,
                        JsonConvert.SerializeObject(khachHang));
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            return View(viewWebLogin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_Email);
            HttpContext.Session.Remove(SessionKey.KhachHang.KH_FullName);
            HttpContext.Session.Remove(SessionKey.KhachHang.KhachHangContext);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KhachHang khachHang)
        {
            try
            {
                _khachHangSvc.AddKhachHang(khachHang);
                return RedirectToAction(nameof(Login), new { id = khachHang.KhanhHangId });
            }
            catch (Exception)
            {
                return View();
            }
        }
        #endregion
        [AuthenticationFilterAttribute_KH]
        public ActionResult Info()
        {
            string KH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_Email);
            if(KH_Email == null || KH_Email == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var khachhangContext = HttpContext.Session.Get(SessionKey.KhachHang.KhachHangContext);
            var khachhangId = JsonConvert.DeserializeObject<KhachHang>(khachhangContext.ToString()).KhanhHangId;
            var khachhang = _khachHangSvc.GetKhachHang(khachhangId);
            return View(khachhang);
        }
        [AuthenticationFilterAttribute_KH]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Info(int khachhangId, KhachHang khachhang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _khachHangSvc.EditKhachHang(khachhangId, khachhang);
                    return RedirectToAction(nameof(Index), new { khachhangId = khachhang.KhanhHangId });
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Cart()
        {
            List<ViewCart> dataCart = new List<ViewCart>();
            var cart = HttpContext.Session.GetString("cart");
            if(cart != null)
            {
                dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
            }
            return View(dataCart);
        }
        [HttpPost]
        public IActionResult UpdateCart(int id, int soluong)
        {
            var cart = HttpContext.Session.GetString("cart");
            double total = 0;
            if(cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if(dataCart[i].MonAn.MonAnId == id)
                    {
                        dataCart[i].Quantity = soluong;
                        break;
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }
        public IActionResult DeleteCart(int id)
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].MonAn.MonAnId == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                total = Tongtien();
                return Ok(total);
            }
            return BadRequest();
        }

        public IActionResult OrderCart()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            if (kH_Email == null || kH_Email == "")  // đã có session
            {
                return BadRequest();
            }
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null && cart.Count() > 0)
            {
                #region DonHang
                var khachhangContext = HttpContext.Session.GetString(SessionKey.KhachHang.KhachHangContext);
                var khachhangId = JsonConvert.DeserializeObject<KhachHang>(khachhangContext).KhanhHangId;

                double total = Tongtien();
                var donhang = new DonHang()
                {
                    TrangthaiDonhang = TrangthaiDonhang.Moidat,
                    KhachHangId = khachhangId,
                    TongTien = total,
                    Ngaydat = DateTime.Now,
                    Ghichu = "",
                };
                _donHangSvc.AddDonHang(donhang);
                int donhangId = donhang.DonHangId;
                #region Chitiet
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    DonhangChitiet chitiet = new DonhangChitiet()
                    {
                        DonHangId = donhangId,
                        MonAnId = dataCart[i].MonAn.MonAnId,
                        Soluong = dataCart[i].Quantity,
                        Thanhtien = dataCart[i].MonAn.Gia * dataCart[i].Quantity,
                        Ghichu = "",
                    };
                    //donhang.DonhangChitiets.Add(chitiet);
                    _donhangChitietSvc.AddDonhangChitietSvc(chitiet);
                }
                #endregion
                #endregion
                HttpContext.Session.Remove("cart");
                return Ok();
            }
            return BadRequest();
        }
        [NonAction]
        private double Tongtien()
        {
            double total = 0;
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ViewCart> dataCart = JsonConvert.DeserializeObject<List<ViewCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    total += (dataCart[i].MonAn.Gia * dataCart[i].Quantity);
                }
            }
            return total;
        }
        [AuthenticationFilterAttribute_KH]
        public ActionResult Details(int id)
        {
            return View(_donHangSvc.GetDonHang(id));
        }

        [AuthenticationFilterAttribute_KH]
        public IActionResult History()
        {
            string kH_Email = HttpContext.Session.GetString(SessionKey.KhachHang.KH_FullName);
            string admin = HttpContext.Session.GetString(SessionKey.NguoiDung.FullName);
            if (kH_Email == null || kH_Email == "" )  // đã có session
            {
                if(admin == null || admin == "" )
                return RedirectToAction("Index", "Home");
            }
            return View(_donHangSvc.GetDonHangAll());
        }

    }
}