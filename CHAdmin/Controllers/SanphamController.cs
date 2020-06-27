using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class SanphamController : BaseController
    {
        // GET: Sanpham
        public ActionResult Danhsachsanpham()
        {
            SanphamDao sanphamDao = new SanphamDao();
            ViewData["danhsachsanpham"] = sanphamDao.getDanhsachsanpham();
            return View();
        }

        public ActionResult Themsanpham()
        {
            LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
            ViewData["danhsachloaisanpham"] = loaisanphamDao.getLoaisanpham();
            
            SelectList listoption = new SelectList(loaisanphamDao.getLoaisanpham().ToList(), "maloaisp", "tenloaisp");
            ViewBag.option = listoption;
            return View();
        }

        [HttpPost]
        public ActionResult Themsanpham(sanpham sanpham)
        {
            SanphamDao sanphamDao = new SanphamDao();
            int ketqua = sanphamDao.postSanpham(sanpham);
            LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
            ViewData["danhsachloaisanpham"] = loaisanphamDao.getLoaisanpham();

            SelectList listoption = new SelectList(loaisanphamDao.getLoaisanpham().ToList(), "maloaisp", "tenloaisp");
            
            ViewBag.option = listoption;
            if (ketqua != 0)
            {
                ViewData["thongbao"] = "ok";
                return View();
            }
            else
            {
                ViewData["thongbao"] = "error";
                return View();
            }
        }

        
        public int Xoasanpham(int masp)
        {
            SanphamDao sanphamDao = new SanphamDao();
            int ketqua = sanphamDao.getXoasanpham(masp);
            return ketqua;
        }

        public ActionResult Suasanpham(int masp)
        {
            LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
            SanphamDao sanphamDao = new SanphamDao();
            ViewData["danhsachsanpham"] = sanphamDao.getSanpham(masp);
            SelectList list= new SelectList(loaisanphamDao.getLoaisanpham().ToList(), "maloaisp", "tenloaisp");
            list.Where(x => x.Text == sanphamDao.getSanpham(masp).tenloaisp).First().Selected = true;
            ViewBag.optionslp = list;
            return View();
        }

        [HttpPost]
        public ActionResult Suasanpham(sanpham sanpham)
        {
            SanphamDao sanphamDao = new SanphamDao();
            if (sanpham.hinhanh == "")
                sanpham.hinhanh = Request["hinhanhcu"].ToString();
            int ketqua = sanphamDao.postSuasanpham(sanpham);
            if (ketqua > 0)
            {
                ViewData["thongbao"] = "ok";
                LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
                ViewData["danhsachsanpham"] = sanphamDao.getSanpham((int)sanpham.masp);
                SelectList list = new SelectList(loaisanphamDao.getLoaisanpham().ToList(), "maloaisp", "tenloaisp");
                list.Where(x => x.Text == sanphamDao.getSanpham((int)sanpham.masp).tenloaisp).First().Selected = true;
                ViewBag.optionslp = list;
                return View();
            }
            else
            {
                ViewData["thongbao"] = "error";
                LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
                ViewData["danhsachsanpham"] = sanphamDao.getSanpham((int)sanpham.masp);
                SelectList list = new SelectList(loaisanphamDao.getLoaisanpham().ToList(), "maloaisp", "tenloaisp");
                list.Where(x => x.Text == sanphamDao.getSanpham((int)sanpham.masp).tenloaisp).First().Selected = true;
                ViewBag.optionslp = list;
                return View();
            }
        }
    }
}