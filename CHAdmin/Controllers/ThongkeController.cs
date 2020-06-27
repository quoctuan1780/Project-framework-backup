using Microsoft.Ajax.Utilities;
using Model.Dao;
using Model.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class ThongkeController : Controller
    {
        ThongkeDao thongkeDao = new ThongkeDao();
        // GET: Thongke
        public ActionResult Thongketheobieudo()
        {
            ViewData["nams"] = thongkeDao.getNam();
            return View();
        }

        public string DoanhthutheolspNam(int nam)
        {
            string ketqua = thongkeDao.getDoanhthutheolspnam(nam);
            return ketqua;
        }

        public string Doanhthutheoloaisanpham()
        {
            string ketqua = thongkeDao.getDoanhthutheoloaisanpham();
            return ketqua;
        }

        public string Doanhthutheosanpham(int nam)
        {
            string ketqua = thongkeDao.getDoanhthutheosanpham(nam);
            return ketqua;
        }

        public ActionResult Thongketheohoadon()
        {
            return View();
        }

        
        public string Doanhthutheohoadon(string date, int value)
        {
            string temp = HttpUtility.UrlDecode(date);
            if (value != 1)
            {
                var data = (JObject)JsonConvert.DeserializeObject(date);
                string batdau = data["batdau"].Value<string>();
                string ketthuc = data["ketthuc"].Value<string>();
                if (value == 2)
                    return JsonConvert.SerializeObject(thongkeDao.getDoanhthuhoadonthang(batdau, ketthuc).ToList());
                else
                    return JsonConvert.SerializeObject(thongkeDao.getDoanhthuhoadonnam(batdau, ketthuc).ToList());
            }
            string Date = "";
            for(int i = 0; i < temp.Length; i++)
            {
                if (i == temp.Length - 2)
                {
                    break;
                }
                Date = Date + temp[i + 1];
            }
            if(value == 1)
            {
                var ketqua = thongkeDao.getDoanhthutheohoadonngay(Date);
                return JsonConvert.SerializeObject(ketqua.ToList());
            }
            return "";
        }
    }
}