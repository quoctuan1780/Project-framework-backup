using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class LoaisanphamController : BaseController
    {
        LoaisanphamDao loaisanphamDao = new LoaisanphamDao();
        // GET: Loaisanpham
        public ActionResult Danhsachloaisanpham()
        {
            ViewData["loaisp"] = loaisanphamDao.getLoaisanpham();
            return View();
        }

        public ActionResult Themloaisanpham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Themloaisanpham(loaisanpham lsp)
        {
            int ketqua = loaisanphamDao.postLoaisanpham(lsp);
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

        public int Xoaloaisanpham(int maloaisp)
        {
            int ketqua = loaisanphamDao.getXoaloaisanpham(maloaisp);
            return ketqua;
        }

        public ActionResult Sualoaisanpham(int maloaisp)
        {
            ViewData["loaisanpham"] = loaisanphamDao.getLoaisanpham(maloaisp);
            return View();
        }

        [HttpPost]
        public ActionResult Sualoaisanpham(loaisanpham lsp)
        {
            loaisanpham loaisp = new loaisanpham();
            lsp.maloaisp = int.Parse(Request["maloaisp"].ToString());
            lsp.tenloaisp = Request["tenloaisp"].ToString();
            lsp.hinhanh = null;
            lsp.mota = null;
            if (loaisanphamDao.Kiemtratontailoaisanpham(lsp))
            {
                ViewData["thongbao"] = "error";
                ViewData["loaisanpham"] = loaisanphamDao.getLoaisanpham((int)lsp.maloaisp);
            }
            else
            {
                int ketqua = loaisanphamDao.postSualoaisanpham(lsp);
                if (ketqua > 0) ViewData["thongbao"] = "ok";
                ViewData["loaisanpham"] = loaisanphamDao.getLoaisanpham((int)lsp.maloaisp);
            }
            return View();
        }
    }
}