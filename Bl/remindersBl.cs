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

        //הוספה  תזכורת לרשימה
        public static void addReminderDetails(remindersEntities r)
        {
            remindersDal.AddReminders(remindersEntities.ConvertToDb(r));
        }
    }
}
