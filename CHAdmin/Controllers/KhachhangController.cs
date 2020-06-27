using Model.Dao;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CHAdmin.Controllers
{
    public class KhachhangController : BaseController
    {
        // GET: Khachhang
        public ActionResult Danhsachkhachhang()
        {
            KhachhangDao khachhangDao = new KhachhangDao();
            var dskh = khachhangDao.getKhachhang();
            ViewData["dskh"] = dskh;
            return View();
        }

        public ActionResult Dangkinhantinmoi()
        {
            KhachhangDao khachhangDao = new KhachhangDao();
            var dkntMoi = khachhangDao.getDangkinhantinmoi();
            ViewData["dkntmoi"] = dkntMoi;
            return View();
        }

        public ActionResult Dangkinhantin()
        {
            KhachhangDao khachhangDao = new KhachhangDao();
            var dknt = khachhangDao.getDangkinhantin();
            ViewData["dknt"] = dknt;
            return View();
        }

        public ActionResult Guitin(string data)
        {
            List<string> danhsachemail = JsonConvert.DeserializeObject<List<string>>(data);
            ViewData["danhsachemail"] = danhsachemail;
            return View();
        }

        [HttpPost]
        public ActionResult Guitin()
        {
            string tieude = Request["tieude"].ToString();
            string noidung = Request["noidung"].ToString();
            List<string> danhsachemail = JsonConvert.DeserializeObject<List<string>>(Request["danhsachemail"]);
            try
            {
                ViewData["thongbao"] = "ok";
                ViewData["danhsachemail"] = danhsachemail;
                sendMail(danhsachemail, tieude, noidung);
            }catch(Exception e)
            {
                ViewData["danhsachemail"] = danhsachemail;
                ViewData["thongbao"] = "error";
            }
            return View();
        }

        public void sendMail(List<string> danhsachemail, string tieude, string noidung)
        {
            var EmailGui = ConfigurationManager.AppSettings["EmailGui"].ToString();
            var EmailGuiTenHienThi = "Coffee House";
            var EmailMatkhau = ConfigurationManager.AppSettings["EmailMatkhau"].ToString();
            var SMTHost = ConfigurationManager.AppSettings["SMTHost"].ToString();
            var SMTPort = int.Parse(ConfigurationManager.AppSettings["SMTPort"].ToString());
            bool SSL = bool.Parse(ConfigurationManager.AppSettings["SSL"].ToString());
            string subject = tieude;
            string body = noidung;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(EmailGui, EmailGuiTenHienThi);
            foreach(string email in danhsachemail)
            {
                mailMessage.To.Add(new MailAddress(email));
            }
            mailMessage.IsBodyHtml = false;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(EmailGui, EmailMatkhau);
            client.Host = SMTHost;
            client.Port = SMTPort;
            client.EnableSsl = SSL;
            client.Send(mailMessage);
        }
    }
}