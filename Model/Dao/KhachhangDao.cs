using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class KhachhangDao
    {
        CoffeeHouseDbContext db = null;

        public KhachhangDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public struct khachhangtaikhoan
        {
            public int makh;
            public string tenkh;
            public string gioitinh;
            public string diachi;
            public string sdt;
            public string email;
            public string tentk;
        }

        public List<khachhangtaikhoan> getKhachhang()
        {
            var truyvan = from kh in db.khachhangs join
                            tk in db.users on (int)kh.matk equals (int)tk.id into g
                            from khGroup in g.DefaultIfEmpty()
                            select new
                            {
                                tentk = khGroup == null ? "null" : khGroup.tentk,
                                kh.makh,
                                kh.hoten,
                                kh.diachi,
                                kh.gioitinh,
                                kh.email,
                                kh.sodt
                              
                            };
                          
            List<khachhangtaikhoan> listKhtk = new List<khachhangtaikhoan>();
            foreach(var dt in truyvan)
            {
                khachhangtaikhoan khtk = new khachhangtaikhoan();
                khtk.makh = (int)dt.makh;
                khtk.tenkh = dt.hoten;
                khtk.gioitinh = dt.gioitinh;
                khtk.diachi = dt.diachi;
                khtk.sdt = dt.sodt;
                khtk.email = dt.email;
                khtk.tentk = dt.tentk;
                listKhtk.Add(khtk);
            }
            return listKhtk;
        }
        public IQueryable<dknt> getDangkinhantinmoi()
        {
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-DD-mm");
            var dkntMoi = db.dknts.Where(x => x.ngaydk == date);
            return dkntMoi;
        }

        public IQueryable<dknt> getDangkinhantin()
        {
            var dkntMoi = db.dknts;
            return dkntMoi;
        }

        public List<khachhangtaikhoan> getLoaikhachhang(int lkh)
        {
            List<khachhangtaikhoan> listKhtk = new List<khachhangtaikhoan>();
            if (lkh == 1)
            {
                var truyvan = from kh in db.khachhangs join
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
                    khachhangtaikhoan khtk = new khachhangtaikhoan();
                    khtk.makh = (int)dt.makh;
                    khtk.tenkh = dt.hoten;
                    khtk.gioitinh = dt.gioitinh;
                    khtk.diachi = dt.diachi;
                    khtk.sdt = dt.sodt;
                    khtk.email = dt.email;
                    khtk.tentk = dt.tentk;
                    listKhtk.Add(khtk);
                }
            }
            else
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
                foreach(var dt in truyvan)
                {
                    khachhangtaikhoan khtk = new khachhangtaikhoan();
                    khtk.makh = (int)dt.makh;
                    khtk.tenkh = dt.hoten;
                    khtk.gioitinh = dt.gioitinh;
                    khtk.diachi = dt.diachi;
                    khtk.sdt = dt.sodt;
                    khtk.email = dt.email;
                    khtk.tentk = "Không có";
                    listKhtk.Add(khtk);
                }
            }
            return listKhtk;
        }
    }
}
