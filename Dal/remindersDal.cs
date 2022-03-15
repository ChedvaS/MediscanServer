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
            return db.REMINDERStbl.ToList();
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
             db.REMINDERStbl.FirstOrDefault(x => x.ID == r.ID).IDDETAIL = r.IDDETAIL;
             db.REMINDERStbl.FirstOrDefault(x => x.ID == r.ID).DATETAKE = r.DATETAKE;
             db.REMINDERStbl.FirstOrDefault(x => x.ID == r.ID).HOURTAKE = r.HOURTAKE;
            db.REMINDERStbl.FirstOrDefault(x => x.ID == r.ID).GMAIL = r.GMAIL;

            db.SaveChanges();
        }
       //פונקציה לשליפת תזיכורת פעילות לפי מייל
       public static List<activityReminders> GetActivityRemindersByGmail(string gmail)
        {
            List<activityReminders> listActive = new List<activityReminders>();
            var lista = db.REMINDERStbl.Where(x => x.GMAIL == gmail).Select(x => new { namemedicine = x.REMINDERDETAILStbl.MEDICINESTOCKtbl.MEDICINEtbl.NAMEMEDICINE, startDate = x.REMINDERDETAILStbl.STARTDATE, numDays = x.REMINDERDETAILStbl.AMOUNTDAYS, hourTake = x.HOURTAKE }).ToList();
            foreach (var item in lista)
            {
                double nDays = double.Parse(item.numDays.ToString());
                double LeftD = (item.startDate.Value.AddDays(nDays) - DateTime.Today).TotalDays;
                if (LeftD > 0)
                    listActive.Add(new activityReminders { LeftDays = short.Parse(LeftD.ToString()), MedicineName = item.namemedicine, NextTakingTime = item.hourTake });
            }

            return listActive;
        }
    }
}
