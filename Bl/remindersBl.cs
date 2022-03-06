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
            return remindersEntities.convertToEntities(remindersDal.Getall().FirstOrDefault(x => x.ID == idr));
        }
        //הוספה  תזכורת לרשימה
        public static void addReminder(remindersEntities r)
        {
            remindersDal.AddReminders(remindersEntities.ConvertToDb(r));
        }
    }
}
