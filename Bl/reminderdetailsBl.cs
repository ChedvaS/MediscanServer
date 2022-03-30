using Entities;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class reminderdetailsBl
    {
        //שליפת רשימת פרטי התזכורות
        public static List<reminderdetailsEntities> GetReminderDetailsList()
        {
            return reminderdetailsEntities.ConvertToListEntities(reminderdetailsDal.Getall());
        }

        //שליפת פרטי תזכורת לפי קוד
        public static reminderdetailsEntities GetReminderDetailsById(int idrd)
        {
            return reminderdetailsEntities.convertToEntities(reminderdetailsDal.Getall().FirstOrDefault(x => x.ID == idrd));
        }

        //הוספה פרטי תזכורת לרשימה
        public static short addReminderDetails(reminderdetailsEntities r)
        {
            var result = reminderdetailsDal.AddReminderDetails(reminderdetailsEntities.ConvertToDb(r));
            return result;
        }

        //עדכון פרטי תזכורת ברשימה
        public static void updateReminderDetails(reminderdetailsEntities r)
        {
            reminderdetailsDal.update(reminderdetailsEntities.ConvertToDb(r));
        }

        //הסרת פרטי תזכורת מהרשימה
        public static bool deleteReminderDetails(int id)
        {
            return reminderdetailsDal.delete(id);
        }
    }
}
