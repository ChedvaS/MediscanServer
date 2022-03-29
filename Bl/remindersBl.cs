using Dal;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class remindersBl
    {
        //שליפת רשימת  התזכורות
        public static List<remindersEntities> GetReminderList()
        {
            return remindersEntities.ConvertToListEntities(remindersDal.Getall());
        }

        //שליפת  תזכורת לפי קוד
        public static remindersEntities GetReminderById(int idr)
        {
            var reminders = remindersDal.Getall();
            if (reminders != null)
                return remindersEntities.convertToEntities(reminders.FirstOrDefault(x => x.ID == idr));
            return null;
        }
        //הוספה  תזכורת לרשימה
        public static void addReminder(remindersEntities r)
        {
            remindersDal.AddReminders(remindersEntities.ConvertToDb(r));
        }
        //   מחיקת תיזכורות לפי 

        public static void deleteReminderDetails(int id)
        {
            reminderdetailsDal.delete(id);
        }


        //שליפת רשמית תזכורות לפי מייל
        public static List<remindersEntities> GetReminderByGmail(string gmail)
        {
            var listr = remindersDal.Getall().Where(x => x.GMAIL == gmail).ToList();

            if (listr != null)
                return remindersEntities.ConvertToListEntities(listr);
            return null;
        }
    }
}
