using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Newtonsoft.Json;

namespace CHAdmin.Controllers
{
    public class AjaxController : Controller
    {
        AjaxDao ajaxDao = new AjaxDao();
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        public string getSanpham()
        {
            var list = ajaxDao.GetDoanhthutheosanpham();
            var json = JsonConvert.SerializeObject(list);
            return json;
        } 

        public string getLoaikhachhang(int lkh)
        {
            return ajaxDao.getLoaikhachhang(lkh);
        }

        public string getChitiethoadon(int mahd)
        {
            return ajaxDao.getChitiethoadon(mahd);
        }

        public string getNoidungphanhoi(int maph)
        {
            AjaxDao ajaxDao = new AjaxDao();
            string phanhoi = ajaxDao.getNoidungphanhoi(maph);
            return phanhoi;
        }
    }
}