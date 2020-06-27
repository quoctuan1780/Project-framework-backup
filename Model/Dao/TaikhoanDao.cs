using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class TaikhoanDao
    {
        CoffeeHouseDbContext db = null;

        public TaikhoanDao()
        {
            db = new CoffeeHouseDbContext();
        }
        public long Insert(user entity)
        {
            db.users.Add(entity);
            db.SaveChanges();
            return entity.id;
        }
        public user getThongtintaikhoan(string userName)
        {
            return db.users.SingleOrDefault(x => x.email == userName);
        }

        public IQueryable<user> getDanhsachtaikhoan()
        {
            var user = db.users;
            return user;
        }
        public user Login(string userName, string password)
        {
            user result = null;
            result = db.users.Where(x => x.email == userName && x.password == password).FirstOrDefault();
            return result;
        }

        public void setTrangthaidangnhap(user taikhoan, int ttdn)
        {
            taikhoan.ttdn = ttdn;
            db.users.AddOrUpdate(taikhoan);
            db.SaveChanges();
        }

        public bool Kiemtramatkhau(string email, string matkhau)
        {
            var ketqua = db.users.Count(x => x.email == email && x.password == matkhau);
            if (ketqua > 0) return true;
            return false;
        }

        public int postDoimatkhau(string email, string matkhau)
        {
            user us = db.users.Where(x => x.email == email).FirstOrDefault();
            us.password = matkhau;
            db.users.AddOrUpdate(us);
            return db.SaveChanges();
        }

        public bool Kiemtrataikhoan(string email)
        {
            int ketqua = db.users.Count(x => x.email == email);
            if (ketqua > 0) return true;
            return false;
        }

        public int postSuataikhoan(user user)
        {
            db.users.AddOrUpdate(user);
            return db.SaveChanges();
        }

        public int postThemtaikhoan(user user)
        {
            db.users.Add(user);
            return db.SaveChanges();
        }
    }
}
