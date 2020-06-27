using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace CHAdmin.Controllers
{
    public class DonhangController : BaseController
    {
        DonhangDao donhangDao = new DonhangDao();
        // GET: Donhang
        public ActionResult Danhsachdonhang()
        {
            var donhang = donhangDao.getDonhang();
            ViewData["donhang"] = donhang;
            return View();
        }

        [HttpGet]
        public ActionResult Chitietdonhang(string madh)
        {
            int ma = int.Parse(madh);
            var khachhang = donhangDao.getKhachhangDonhang(ma);
            var ctdh = donhangDao.getChitietdonhang(ma);
            ViewData["khachhangdonhang"] = khachhang;
            ViewData["ctdh"] = ctdh;
            ViewData["madh"] = madh;
            return View();
        }

        public string Thanhtoandonhang(int madh)
        {
            bool kiemtraThanhtoan = donhangDao.Thanhtoandonhang(madh);
            if (kiemtraThanhtoan) return "ok";
            else
                return "thanhtoanloi";
        }
        public string Donhangtheotrangthai(int tt)
        {
            AjaxDao ajaxDao = new AjaxDao();
            string json = ajaxDao.getTimkiemtrangthaiAjax(tt);
            return json;
        }

        public int Xoadonhang(int madh)
        {
            int ketqua = donhangDao.Xoadonhang(madh);
            return ketqua;
        }
    }
}