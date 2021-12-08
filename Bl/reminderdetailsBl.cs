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
        public static void addReminderDetails(reminderdetailsEntities r)
        {
            reminderdetailsDal.AddReminderDetails(reminderdetailsEntities.ConvertToDb(r));
        }

        //עדכון פרטי תזכורת ברשימה
        public static void updateReminderDetails(reminderdetailsEntities r)
        {
            reminderdetailsDal.update(reminderdetailsEntities.ConvertToDb(r));
        }

        //הסרת פרטי תזכורת מהרשימה
        public static void deleteReminderDetails(int id)
        {
            reminderdetailsDal.delete(id);
        }
    }
}
