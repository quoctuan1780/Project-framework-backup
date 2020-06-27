using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class TrangchuDao
    {
        CoffeeHouseDbContext db = null;

        public TrangchuDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public int getDangkinhantinmoi()
        {
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-MM-dd");
            var result = db.dknts.Where(x => x.ngaydk == date);
            int count = 0;
            foreach (dknt dh in result) count++;
            return count;
        }
        public int getDonhangmoi()
        {
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-MM-dd");
            var result = db.donhangs.Where(x => x.ngaydat == date);
            int count = 0;
            foreach (donhang dh in result) count++;
            return count;
        }

        public int getPhanhoimoi()
        {
            DateTime date = DateTime.Now.Date;
            date.ToString("yyyy-MM-dd");
            var result = db.phanhois.Where(x => x.ngayph == date);
            int count = 0;
            foreach (phanhoi dh in result) count++;
            return count;
        }
    }
}
