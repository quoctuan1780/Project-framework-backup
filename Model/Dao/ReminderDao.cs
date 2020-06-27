using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ReminderDao
    {
        CoffeeHouseDbContext db = null;
        public ReminderDao()
        {
            db = new CoffeeHouseDbContext();
        }

        public int setMakhoiphuc(int user_id, string makhoiphuc)
        {
            var ketqua = db.reminders.Count(x => x.user_id == user_id);
            if (ketqua > 0) return 0;
            reminder reminder = new reminder();
            reminder.user_id = user_id;
            reminder.code = makhoiphuc;
            reminder.completed_at = null;
            db.reminders.Add(reminder);
            return db.SaveChanges();
        }

        public string getMakhoiphuc(int user_id)
        {
            var ketqua = db.reminders.Where(x => x.user_id == user_id).FirstOrDefault();
            return ketqua.code;
        }

        public int setKhoiphucthanhcong(int user_id)
        {
            var ketqua = db.reminders.Where(x => x.user_id == user_id);
            db.reminders.RemoveRange(ketqua);
            return db.SaveChanges();
        }
    }
}
