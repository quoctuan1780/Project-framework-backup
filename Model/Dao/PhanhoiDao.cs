using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PhanhoiDao
    {
        CoffeeHouseDbContext db = null;
        public PhanhoiDao()
        {
            db = new CoffeeHouseDbContext();
        }
        
        public IQueryable<phanhoi> getPhanhoimoi()
        {
            DateTime Date = DateTime.Now.Date;
            Date.ToString("yyyy-MM-dd");
            var phm = db.phanhois.Where(x => x.ngayph == Date);
            return phm;
        }

        public IQueryable<phanhoi> getDanhsachphanhoi()
        {
            var phanhoi = db.phanhois;
            return phanhoi;
        }

        public int getXoaphanhoi(int maph)
        {
            var phanhoi = db.phanhois.Where(x => x.maph == maph).FirstOrDefault();
            db.phanhois.Attach(phanhoi);
            db.phanhois.Remove(phanhoi);
            return db.SaveChanges();
        }
    }
}
