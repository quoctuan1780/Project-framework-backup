using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model.Dao
{
    public class AjaxDao
    {
        CoffeeHouseDbContext db = null;

        public AjaxDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public struct doanhthutheosp{
            public double y;
            public string label;
        }

        public List<doanhthutheosp> GetDoanhthutheosanpham()
        {
            List<doanhthutheosp> dtList = new List<doanhthutheosp>();
            var result = (from cthd in db.cthds
                          join sp in db.sanphams on cthd.masp equals sp.masp
                          group new { sp, cthd } by new { cthd.masp, sp.tensp } into g
                          select new
                          {
                              id = g.Key,
                              list = g.ToList()
                          });
            doanhthutheosp dtStruct;
            foreach(var a in result)
            {
                double gia = 0;
                foreach(var i in a.list)
                {
                    gia = i.cthd.gia * i.cthd.soluong;
                }
                dtStruct.label = a.id.tensp;
                dtStruct.y = gia;
                dtList.Add(dtStruct);
            }
            return dtList;
        }

        public string getTimkiemtrangthaiAjax(int trangthai)
        {
            var truyvan = from dh in db.donhangs join
                          kh in db.khachhangs on (int)dh.makh equals (int)kh.makh
                          where dh.tttt == trangthai
                          select new
                          {
                              kh.hoten,
                              dh.madh,
                              dh.ngaydat,
                              dh.tongtien,
                              dh.httt,
                              dh.tttt,
                              dh.ghichu
                          };

            var json = JsonConvert.SerializeObject(truyvan.ToList());
            return json;
        }

        public string getLoaikhachhang(int lkh)
        {
            string json = "";
            List<KhachhangDao.khachhangtaikhoan> listKhtk = new List<KhachhangDao.khachhangtaikhoan>();
            if (lkh == 1)
            {
                var truyvan = from kh in db.khachhangs
                              join
                                tk in db.users on (int)kh.matk equals (int)tk.id
                              select new
                              {
                                  tk.tentk,
                                  kh.makh,
                                  kh.hoten,
                                  kh.diachi,
                                  kh.gioitinh,
                                  kh.email,
                                  kh.sodt
                              };
                foreach (var dt in truyvan)
                {
                    KhachhangDao.khachhangtaikhoan khtk = new KhachhangDao.khachhangtaikhoan();
                    khtk.makh = (int)dt.makh;
                    khtk.tenkh = dt.hoten;
                    khtk.gioitinh = dt.gioitinh;
                    khtk.diachi = dt.diachi;
                    khtk.sdt = dt.sodt;
                    khtk.email = dt.email;
                    khtk.tentk = dt.tentk;
                    listKhtk.Add(khtk);
                }
                json = JsonConvert.SerializeObject(listKhtk);
            }
            else if(lkh == 2)
            {
                var truyvan = from kh in db.khachhangs
                              where kh.matk == null
                              select new
                              {
                                  kh.makh,
                                  kh.hoten,
                                  kh.diachi,
                                  kh.gioitinh,
                                  kh.email,
                                  kh.sodt
                              };
                foreach (var dt in truyvan)
                {
                    KhachhangDao.khachhangtaikhoan khtk = new KhachhangDao.khachhangtaikhoan();
                    khtk.makh = (int)dt.makh;
                    khtk.tenkh = dt.hoten;
                    khtk.gioitinh = dt.gioitinh;
                    khtk.diachi = dt.diachi;
                    khtk.sdt = dt.sodt;
                    khtk.email = dt.email;
                    khtk.tentk = "Không có";
                    listKhtk.Add(khtk);
                }
                json = JsonConvert.SerializeObject(listKhtk);
            }
            else
            {
                KhachhangDao khachhangDao = new KhachhangDao();
                var listkh = khachhangDao.getKhachhang().ToList();
                json = JsonConvert.SerializeObject(listkh);
            }
            return json;
        }

        public string getChitiethoadon(int mahd)
        {
            string json = "";
            List<DonhangDao.chitietdonhang> chitiethoadons = new List<DonhangDao.chitietdonhang>();
            var truyvan = (from hd in db.hoadons join
                              ct in db.cthds on hd.mahd equals ct.mahd
                                                       join
                            sp in db.sanphams on ct.masp equals sp.masp
                           where hd.mahd == mahd
                           select new
                           {
                               sp.tensp,
                               sp.hinhanh,
                               ct.soluong,
                               ct.gia,
                               hd.ngaythanhtoan,
                               hd.tongtien,
                               hd.httt
                           });

            foreach (var dulieu in truyvan)
            {
                DonhangDao.chitietdonhang ct = new DonhangDao.chitietdonhang();
                ct.tensp = dulieu.tensp;
                ct.hinhanh = dulieu.hinhanh;
                ct.soluong = dulieu.soluong;
                ct.dongia = dulieu.gia;
                ct.thanhtien = (double)dulieu.tongtien;
                ct.tttt = 1;
                ct.ngaydat = dulieu.ngaythanhtoan.ToString("dd/MM/yyyy");
                ct.httt = dulieu.httt;
                chitiethoadons.Add(ct);
            }
            json = JsonConvert.SerializeObject(chitiethoadons   );
            return json;
        }

        public string getNoidungphanhoi(int maph)
        {
            var phanhoi = db.phanhois.Where(x => x.maph == maph).ToList();
            return JsonConvert.SerializeObject(phanhoi);
        }
    }
}
