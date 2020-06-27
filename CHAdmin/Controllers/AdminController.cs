using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            TrangchuDao tc = new TrangchuDao();
            var soluongdonhang = tc.getDonhangmoi();
            var soluongDknt = tc.getDangkinhantinmoi();
            var soluongPhanhoimoi = tc.getPhanhoimoi();
            ViewData["donhangmoi"] = soluongdonhang;
            ViewData["dkntmoi"] = soluongDknt;
            ViewData["slphanhoimoi"] = soluongPhanhoimoi;
            return View();
        }

        public  ActionResult Phanhoimoi()
        {
            PhanhoiDao phanhoiDao = new PhanhoiDao();
            var phanhoimoi = phanhoiDao.getPhanhoimoi();
            ViewData["phanhoimoi"] = phanhoimoi;
            return View();
        }

        public ActionResult Donhangmoi()
        {
            DonhangDao donhangDao = new DonhangDao();
            var donhangmoi = donhangDao.getDonhangmoi();
            ViewData["donhangmoi"] = donhangmoi;
            return View();
        }
    }
}