using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ThongkeDao
    {
        CoffeeHouseDbContext db;

        public ThongkeDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public struct Doanhthuhoadonthangnam
        {
            public int nam;
            public int thang;
            public float tongtien;
            public string httt;
        };

        public List<int> getNam()
        {
            List<int> nams = new List<int>();
            var ketqua = (from nam in db.hoadons
                          group nam by new
                          {
                              nam.ngaythanhtoan.Year
                          } into g
                          select new
                          {
                              year = g.Key.Year
                          });
            foreach (var i in ketqua)
            {
                nams.Add((int)i.year);
            }
            return nams;
        }

        public string getDoanhthutheolspnam(int nam)
        {
            var ketqua = from hd in db.hoadons
                         join
      ct in db.cthds on hd.mahd equals ct.mahd
                         join sp in db.sanphams on ct.masp equals sp.masp
                         join
lsp in db.loaisanphams on sp.maloaisp equals lsp.maloaisp
                         where (int)hd.ngaythanhtoan.Year == nam
                         group new { hd, sp, lsp, ct } by new
                         {
                             hd.ngaythanhtoan.Year,
                             sp.maloaisp,
                             lsp.tenloaisp
                         } into g
                         select new
                         {
                             y = g.Sum(x => x.ct.soluong * x.ct.gia),
                             label = g.Key.tenloaisp
                         };
            return JsonConvert.SerializeObject(ketqua.ToList());
        }

        public string getDoanhthutheoloaisanpham()
        {
            var ketqua = from hd in db.hoadons
                         join
                         ct in db.cthds on hd.mahd equals ct.mahd
                         join sp in db.sanphams on ct.masp equals sp.masp
                         join
                         lsp in db.loaisanphams on sp.maloaisp equals lsp.maloaisp
                         group new { hd, sp, lsp, ct } by new
                         {
                             sp.maloaisp,
                             lsp.tenloaisp
                         } into g
                         select new
                         {
                             y = g.Sum(x => x.ct.soluong * x.ct.gia),
                             label = g.Key.tenloaisp,
                             y1 = g.Sum(x => x.ct.soluong * x.ct.gia)
                         };
            return JsonConvert.SerializeObject(ketqua.ToList());
        }

        public string getDoanhthutheosanpham(int nam)
        {
            if (nam == 1)
            {
                var ketqua = from hd in db.hoadons
                             join
                             ct in db.cthds on hd.mahd equals ct.mahd
                             join sp in db.sanphams on ct.masp equals sp.masp
                             group new { hd, sp, ct } by new
                             {
                                 sp.masp
                             } into g
                             select new
                             {
                                 y = g.Sum(x => x.ct.soluong * x.ct.gia),
                                 label = g.Select(x => x.sp.tensp),
                             };
                return JsonConvert.SerializeObject(ketqua.ToList());
            }
            else
            {
                var ketqua = from hd in db.hoadons
                             join
                             ct in db.cthds on hd.mahd equals ct.mahd
                             join sp in db.sanphams on ct.masp equals sp.masp
                             where (int)hd.ngaythanhtoan.Year == nam
                             group new { hd, sp, ct } by new
                             {
                                 sp.masp
                             } into g
                             select new
                             {
                                 y = g.Sum(x => x.ct.soluong * x.ct.gia),
                                 label = g.Select(x => x.sp.tensp),
                             };

                return JsonConvert.SerializeObject(ketqua.ToList());
            }
        }
        public List<hoadon> getDoanhthutheohoadonngay(string date)
        {
            DateTime Date = DateTime.Parse(date);
            var hoadon = db.hoadons.Where(x => x.ngaythanhtoan == Date).ToList();
            return hoadon.ToList();
        }

        
        public List<Doanhthuhoadonthangnam> getDoanhthuhoadonthang(string batdau, string ketthuc)
        {
            DateTime Batdau = DateTime.Parse(batdau);
            DateTime Ketthuc = DateTime.Parse(ketthuc);
            var doanhthu = from hd in db.hoadons
                           where hd.ngaythanhtoan >= Batdau && hd.ngaythanhtoan <= Ketthuc
                           group new { hd } by new
                           {
                              hd.ngaythanhtoan.Year,
                              hd.ngaythanhtoan.Month,
                              hd.httt
                           } into g
                           select new
                           {
                               nam = g.Key.Year,
                               thang = g.Key.Month,
                               ht = g.Key.httt,
                               tongtien = g.Select(x => x.hd.tongtien)
                           };
            List<Doanhthuhoadonthangnam> listdt = new List<Doanhthuhoadonthangnam>();
            foreach (var dt in doanhthu)
            {
                Doanhthuhoadonthangnam dttn = new Doanhthuhoadonthangnam();
                dttn.nam = dt.nam;
                dttn.thang = dt.thang;
                dttn.httt = dt.ht.ToString();
                dttn.tongtien = 0;
                foreach (var tt in dt.tongtien)
                {
                    dttn.tongtien = dttn.tongtien + (float)tt;
                }
                dttn.httt = dt.ht;
                listdt.Add(dttn);
            }
            return listdt;
        }

        public List<Doanhthuhoadonthangnam> getDoanhthuhoadonnam(string batdau, string ketthuc)
        {
            DateTime Batdau = DateTime.Parse(batdau);
            DateTime Ketthuc = DateTime.Parse(ketthuc);
            var doanhthu = from hd in db.hoadons
                           where hd.ngaythanhtoan >= Batdau && hd.ngaythanhtoan <= Ketthuc
                           group new { hd } by new
                           {
                               hd.ngaythanhtoan.Year,
                               hd.httt
                           } into g
                           select new
                           {
                               nam = g.Key.Year,
                               ht = g.Key.httt,
                               tongtien = g.Select(x => x.hd.tongtien)
                           };
            List<Doanhthuhoadonthangnam> listdt = new List<Doanhthuhoadonthangnam>();
            foreach (var dt in doanhthu)
            {
                Doanhthuhoadonthangnam dttn = new Doanhthuhoadonthangnam();
                dttn.nam = dt.nam;
                dttn.httt = dt.ht.ToString();
                dttn.tongtien = 0;
                foreach (var tt in dt.tongtien)
                {
                    dttn.tongtien = dttn.tongtien + (float)tt;
                }
                dttn.httt = dt.ht;
                listdt.Add(dttn);
            }
            return listdt;
        }
    }
}
