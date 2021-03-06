using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class remindersDal
    {
        static MedicineProjectEntities db = new MedicineProjectEntities();

        //שליפה
        public static List<REMINDERStbl> Getall()
        {

            try
            {
                var l = db.REMINDERStbl;
                if (l != null)
                    return l.ToList();
                return null;
            }
            catch
            {
                return null;
            }
        }
        //הוספה
        public static void AddReminders(REMINDERStbl r)
        {
            db.REMINDERStbl.Add(r);
            db.SaveChanges();
        }
        //מחיקה
        public static void delete(int id)
        {
            db.REMINDERStbl.Remove(db.REMINDERStbl.FirstOrDefault(k => k.ID == id));
            db.SaveChanges();
        }
        //עידכון
        public static void update(REMINDERStbl r)
        {
            var reminder = db.REMINDERStbl.FirstOrDefault(x => x.ID == r.ID);
            reminder.IDDETAIL = r.IDDETAIL;
            reminder.DATETAKE = r.DATETAKE;
            reminder.HOURTAKE = r.HOURTAKE;
            reminder.GMAIL = r.GMAIL;

            db.SaveChanges();
        }
        //פונקציה לשליפת תזיכורת פעילות לפי מייל
        public static List<activityReminders> GetActivityRemindersByGmail(string gmail)
        {
            List<activityReminders> listActive = new List<activityReminders>();
            var lista = db.REMINDERStbl.Where(x => x.GMAIL == gmail).Select(x => new { id = x.ID, medicineId = x.REMINDERDETAILStbl.MEDICINESTOCKtbl.MEDICINEtbl.ID, reminderDId = x.IDDETAIL, namemedicine = x.REMINDERDETAILStbl.MEDICINESTOCKtbl.MEDICINEtbl.NAMEMEDICINE, startDate = x.REMINDERDETAILStbl.STARTDATE, numDays = x.REMINDERDETAILStbl.AMOUNTDAYS, hourTake = x.HOURTAKE, frequincy = x.REMINDERDETAILStbl.FREQUINCY, comment = x.REMINDERDETAILStbl.COMMENT }).ToList();
            foreach (var item in lista)
            {
                int LeftD = 0;
                DateTime followDate = DateTime.Today;
                double nDays = double.Parse(item.numDays.ToString());
                var today = DateTime.Today;
                if (item.startDate != null)
                    followDate = item.startDate.Value.AddDays(nDays);
                if (item.startDate != null)
                    LeftD = (followDate - today).Days;
                if (LeftD > 0)
                {
                    var result = listActive.FirstOrDefault(x => x.reminderDId == item.reminderDId);
                    if (result == null)
                    {
                        listActive.Add(new activityReminders
                        {
                            MedicineId = item.medicineId,
                            reminderDId = item.reminderDId,
                            LeftDays = short.Parse(LeftD.ToString()),
                            MedicineName = item.namemedicine
                             ,
                            TakingTimes = new Dictionary<int, DateTime?>(),
                            comment = item.comment,
                            frequincy = item.frequincy
                        });
                        var last = listActive.LastOrDefault();
                        last.TakingTimes.Add(item.id, item.hourTake);
                    }
                    else
                        result.TakingTimes.Add(item.id, item.hourTake);
                }
            }
            return listActive;
        }
    }
}
