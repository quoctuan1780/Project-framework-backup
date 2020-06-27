using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class HoadonController : BaseController
    {
        HoadonDao hoadonDao = new HoadonDao();
        // GET: Hoadon
        public ActionResult Danhsachhoadon()
        {
            ViewData["danhsachhoadon"] = hoadonDao.getHoadon();
            return View();
        }
    }
}