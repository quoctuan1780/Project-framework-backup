using Model.EF;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class SanphamDao
    {
        CoffeeHouseDbContext db = null;

        public struct SanPham
        {
            public int masp;
            public string tensp;
            public string tenloaisp;
            public float gia;
            public float giakm;
            public string mota;
            public string hinhanh;
            public string donvitinh;
            public string trangthaisanpham;
        }

        public SanphamDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public List<SanPham> getDanhsachsanpham()
        {
            var sanpham = from sp in db.sanphams
                          join
                            lsp in db.loaisanphams on sp.maloaisp equals lsp.maloaisp
                          select new
                          {
                              sp,
                              lsp
                          };
            List<SanPham> sanPhams = new List<SanPham>();
            foreach(var sp in sanpham)
            {
                SanPham sanPham = new SanPham();
                sanPham.masp = (int)sp.sp.masp;
                sanPham.tensp = sp.sp.tensp;
                sanPham.tenloaisp = sp.lsp.tenloaisp;
                sanPham.gia = (float)sp.sp.gia;
                sanPham.giakm = (float)sp.sp.giakm;
                sanPham.donvitinh = sp.sp.dvt;
                if (sp.sp.moi == 1)
                    sanPham.trangthaisanpham = "Mới";
                else sanPham.trangthaisanpham = "Cũ";
                sanPhams.Add(sanPham);
            }
            return sanPhams;
        }

        public int postSanpham(sanpham sp)
        {
            List<sanpham> kiemtratontai = db.sanphams.Where(x => x.tensp == sp.tensp).ToList();
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-MM-dd");
            if (kiemtratontai.Count > 0)
            {
                return 0;
            }
            else
            {
                sp.ngaynhap = date;
                if (sp.mota == null) sp.mota = "";
                if (sp.hinhanh == null) sp.hinhanh = "";
                sp.moi = 1;
                db.sanphams.Add(sp);
                return db.SaveChanges();
            }
        }

        public SanPham getSanpham(int masp)
        {
            var sanpham = from s in db.sanphams join 
                            lsp in db.loaisanphams on s.maloaisp equals lsp.maloaisp
                          where s.masp == masp
                          select new
                          {
                              s,
                              lsp
                          };
            SanPham sanPham = new SanPham();
            var sp = sanpham.FirstOrDefault();
            sanPham.masp = (int)sp.s.masp;
            sanPham.tensp = sp.s.tensp;
            sanPham.tenloaisp = sp.lsp.tenloaisp;
            sanPham.gia = (float)sp.s.gia;
            sanPham.giakm = (float)sp.s.giakm;
            sanPham.donvitinh = sp.s.dvt;
            sanPham.mota = sp.s.mota;
            sanPham.hinhanh = sp.s.hinhanh;
            if (sp.s.moi == 1)
                sanPham.trangthaisanpham = "Mới";
            else sanPham.trangthaisanpham = "Cũ";
            return sanPham;
        }

        public int postSuasanpham(sanpham sanpham)
        {
            var kiemtratontai = db.sanphams.Where(x => x.tensp == sanpham.tensp && x.masp != sanpham.masp).ToList();
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-MM-dd");
            if (kiemtratontai.Count > 0)
            {
                return 0;
            }
            else
            {
                if (sanpham.mota == null) sanpham.mota = "";
                if (sanpham.hinhanh == null) sanpham.hinhanh = "";
                sanpham.moi = 1;
                sanpham.ngaynhap = date;
                db.sanphams.AddOrUpdate(sanpham);
                db.SaveChanges();
                return 1;
            }
        }

        public int getXoasanpham(int masp)
        {
            ctdh kiemtractdh = db.ctdhs.Where(x => x.masp == masp).FirstOrDefault();
            cthd kiemtracthd = db.cthds.Where(x => x.masp == masp).FirstOrDefault();
            
            if (kiemtractdh == null && kiemtracthd == null)
            {
                sanpham sp = db.sanphams.Where(x => x.masp == masp).FirstOrDefault();
                db.sanphams.Attach(sp);
                db.sanphams.Remove(sp);
                return db.SaveChanges();
            }
            else
                return 0;
        }
    }
}
