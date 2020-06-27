using CHAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using CHAdmin.Common;
using Org.BouncyCastle.Asn1.X509;
using System.Net.Mail;
using System.Configuration;
using MySql.Data.MySqlClient.Memcached;
using System.Net;
using Org.BouncyCastle.Ocsp;

namespace CHAdmin.Controllers
{
    public class AccountController : Controller
    {
        TaikhoanDao taikhoanDao = new TaikhoanDao();
        ReminderDao reminderDao = new ReminderDao();
        // GET: 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(loginModel model)
        {
            user result = taikhoanDao.Login(model.userName, Encrypt.MD5Hash(model.password));
            if (result != null && result.password.Equals(Encrypt.MD5Hash(model.password)))
            {
                var user = taikhoanDao.getThongtintaikhoan(model.userName);
                taikhoanDao.setTrangthaidangnhap(user, 1);
                Session.Add(Constants.USER_SESSION, user);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Email hoặc password không đúng");
            }
            return View(model);
        }

        public void sendMail(string email, string code)
        {
            var EmailGui = ConfigurationManager.AppSettings["EmailGui"].ToString();
            var EmailGuiTenHienThi = ConfigurationManager.AppSettings["EmailGuiTenhienthi"].ToString();
            var EmailMatkhau = ConfigurationManager.AppSettings["EmailMatkhau"].ToString();
            var SMTHost = ConfigurationManager.AppSettings["SMTHost"].ToString();
            var SMTPort = int.Parse(ConfigurationManager.AppSettings["SMTPort"].ToString());
            bool SSL = bool.Parse(ConfigurationManager.AppSettings["SSL"].ToString());
            string subject = "Khôi phục mật khẩu";
            string body = "Xin chào " + email + ", đây là email được gửi từ Admin để khôi phục mật khẩu cho bạn " +
                "\nXin hãy nhấn vào link dưới để tiến hành khôi phục mật khẩu\n" +
                "<a href=" + $"'https://localhost:44392/Account/Khoiphucmatkhau?code={code}&email={email}'" + ">Click vào đây</a>";
            MailMessage mailMessage = new
                MailMessage(new MailAddress(EmailGui, EmailGuiTenHienThi), new MailAddress(email));
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(EmailGui, EmailMatkhau);
            client.Host = SMTHost;
            client.Port = SMTPort;
            client.EnableSsl = SSL;
            client.Send(mailMessage);
        }

        public ActionResult Khoiphucmatkhau(string code, string email)
        {
            ViewData["email"] = email;
            ViewData["maphuchoi"] = code;
            return View();
        }

        [HttpPost]
        public ActionResult Khoiphucmatkhau()
        {
            string code = Request["code"] as string;
            string email = Request["email_user"] as string;
            string matkhau = Request["password"] as string;
            string nhaplai = Request["re_password"] as string;
            var user = taikhoanDao.getThongtintaikhoan(email);
            int id = (int)user.id;
            if (!matkhau.Equals(nhaplai))
            {
                ViewData["thongbao"] = "error";
            }
            else
            {
                int ketqua = reminderDao.setKhoiphucthanhcong(id);
                if (ketqua > 0)
                {
                    taikhoanDao.postDoimatkhau(email, Encrypt.MD5Hash(matkhau));
                    ViewData["thongbao"] = "ok";
                    return View();
                }
                else ViewData["thongbao"] = "error_code";
            }
            return View();
        }

        public ActionResult Quenmatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quenmatkhau(string a)
        {
            string email = Request["email"] as string;
            bool kiemtrataikhoan = taikhoanDao.Kiemtrataikhoan(email);
            if (kiemtrataikhoan)
            {
                var user = taikhoanDao.getThongtintaikhoan(email);
                int id = (int)user.id;
                ViewData["thongbao"] = "ok";
                Random random = new Random();
                int kiemtramakp = reminderDao.setMakhoiphuc(id, Encrypt.MD5Hash(random.Next(1000, 2000).ToString()));
                if(kiemtramakp > 0)
                {
                    string code = reminderDao.getMakhoiphuc(id);
                    sendMail(email, code);
                }
                else
                {
                    string code = reminderDao.getMakhoiphuc(id);
                    sendMail(email, code);
                }
            }
            else ViewData["thongbao"] = "error";
            return View();
        }
        public ActionResult Thongtintaikhoan()
        { 
            return View();
        }

        public ActionResult Doimatkhau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Doimatkhau(string b)
        {
            Model.EF.user user = Session[CHAdmin.Common.Constants.USER_SESSION] as Model.EF.user;
            string matkhaucu = Request["Matkhaucu"] as string;
            bool kiemtra = taikhoanDao.Kiemtramatkhau(user.email, Encrypt.MD5Hash(matkhaucu));
            if (kiemtra)
            {
                string matkhaumoi = Request["Maukhaumoi"] as string;
                string nhaplai = Request["Nhaplaimatkhau"] as string;
                if (matkhaumoi == nhaplai)
                {
                    int ketqua = taikhoanDao.postDoimatkhau(user.email, Encrypt.MD5Hash(nhaplai));
                    if (ketqua != 0)
                    {
                        ((user)Session[Constants.USER_SESSION]).password = Encrypt.MD5Hash(nhaplai);
                        ViewData["thongbao"] = "ok";
                    }
                }
                else ViewData["thongbao"] = "error_matkhau_new";

            }
            else
            {
                ViewData["thongbao"] = "error_matkhaucu";
            }
            return View();
        }

        public ActionResult Danhsachtaikhoan()
        {
            TaikhoanDao taikhoanDao = new TaikhoanDao();
            ViewData["danhsachtk"] = taikhoanDao.getDanhsachtaikhoan();
            return View();
        }

        public ActionResult Themtaikhoan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Themtaikhoan(user user)
        {
            string re_password = Request["re_password"].ToString();
            string password = Request["password"].ToString();
            string email = Request["email"].ToString();
            if(taikhoanDao.getThongtintaikhoan(email) != null)
            {
                ViewData["thongbao"] = "email";
                return View();
            }
            if (!re_password.Equals(password))
            {
                ViewData["thongbao"] = "password";
                return View();
            }
            user tk = new user();
            tk.email = email;
            tk.tentk = Request["tentk"].ToString();
            tk.password = Encrypt.MD5Hash(password);
            tk.maquyen = int.Parse(Request["maquyen"].ToString());
            if (Request["hinhanh"] == "")
                tk.hinhanh = "Aelita.jpg";
            else tk.hinhanh = Request["hinhanh"].ToString();
            tk.ttdn = 0;
            int ketqua = taikhoanDao.postThemtaikhoan(tk);
            if(ketqua > 0)
                ViewData["thongbao"] = "ok";
            return View();
        }

        public ActionResult Suataikhoan(string email)
        {
            ViewData["thongtintaikhoan"] = taikhoanDao.getThongtintaikhoan(email);
            return View();
        }

        [HttpPost]
        public ActionResult Suataikhoan(user taikhoan)
        {
            user tk = new user();
            tk.id = int.Parse(Request["id"].ToString());
            tk.email = Request["email"].ToString();
            tk.tentk = Request["tentk"].ToString();
            tk.password = Request["password"].ToString();
            tk.maquyen = int.Parse(Request["maquyen"].ToString());
            if (Request["hinhanh"] != "")
                tk.hinhanh = Request["hinhanh"].ToString();
            else tk.hinhanh = Request["hinhanhcu"].ToString();
            tk.ttdn = int.Parse(Request["ttdn"].ToString());
            int ketqua = taikhoanDao.postSuataikhoan(tk);
            if (ketqua > 0)
            {
                ViewData["thongtintaikhoan"] = taikhoanDao.getThongtintaikhoan(tk.email);
                ViewData["thongbao"] = "ok";
            }
            else
            {
                ViewData["thongtintaikhoan"] = taikhoanDao.getThongtintaikhoan(tk.email);
                ViewData["thongbao"] = "error";
            }
            return View();
        }

        public ActionResult Logout()
        {
            user taikhoan = Session[Constants.USER_SESSION] as user;
            taikhoanDao.setTrangthaidangnhap(taikhoan, 0);
            Session.Clear();
            return Redirect("/Account/Login");
        }
    }
}