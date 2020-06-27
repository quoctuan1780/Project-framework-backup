using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public struct hoadonKhachhang
    {
        public int mahd;
        public string tenkh;
        public DateTime ngaythanhtoan;
        public float tongtien;
        public string httt;
    }
    public class HoadonDao
    {
        CoffeeHouseDbContext db = null;

        public HoadonDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public List<hoadonKhachhang> getHoadon()
        {
            var ketqua = from hd in db.hoadons
                         join
                         kh in db.khachhangs on hd.makh equals kh.makh
                         select new { hd, kh };
            List<hoadonKhachhang> hoadons = new List<hoadonKhachhang>();
            foreach(var kq in ketqua)
            {
                hoadonKhachhang hdkh = new hoadonKhachhang();
                hdkh.mahd = (int)kq.hd.mahd;
                hdkh.tenkh = kq.kh.hoten;
                hdkh.ngaythanhtoan = kq.hd.ngaythanhtoan;
                hdkh.tongtien = (float)kq.hd.tongtien;
                hdkh.httt = kq.hd.httt;
                hdkh.ngaythanhtoan.Date.ToString("dd/MM/yyyy");
                hoadons.Add(hdkh);
            }
            return hoadons;
        }
    }
}
