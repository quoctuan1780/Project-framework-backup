using Model.Dao;
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
    public class PhanhoiController : Controller
    {
        PhanhoiDao phanhoiDao = new PhanhoiDao();
        // GET: Phanhoi
        public ActionResult Danhsachphanhoi()
        {
            ViewData["danhsachphanhoi"] = phanhoiDao.getDanhsachphanhoi();
            return View();
        }

        public int Xoaphanhoi(int maph)
        {
            int ketqua = phanhoiDao.getXoaphanhoi(maph);
            return ketqua;
        }

        public ActionResult Phanhoikhachhang(string email)
        {
            ViewData["email"] = email;
            return View();
        }

        [HttpPost]
        public ActionResult Phanhoikhachhang()
        {
            string email = Request["email"].ToString();
            string tieude = Request["tieude"].ToString();
            string noidung = Request["noidung"].ToString();
            try
            {
                sendMail(email, tieude, noidung);
                ViewData["thongbao"] = "ok";
                ViewData["email"] = email;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                ViewData["email"] = email;
                ViewData["thongbao"] = "error";
            }
            return View();
        }

        public void sendMail(string email, string tieude, string noidung)
        {
            var EmailGui = ConfigurationManager.AppSettings["EmailGui"].ToString();
            var EmailGuiTenHienThi = "Coffee House";
            var EmailMatkhau = ConfigurationManager.AppSettings["EmailMatkhau"].ToString();
            var SMTHost = ConfigurationManager.AppSettings["SMTHost"].ToString();
            var SMTPort = int.Parse(ConfigurationManager.AppSettings["SMTPort"].ToString());
            bool SSL = bool.Parse(ConfigurationManager.AppSettings["SSL"].ToString());
            string subject = tieude;
            string body = noidung;
            MailMessage mailMessage = new
                MailMessage(new MailAddress(EmailGui, EmailGuiTenHienThi), new MailAddress(email));
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