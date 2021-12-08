using Dal;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class usersBl
    {
        //הוספה לקוח לרשימה
        public static void addReminderDetails(useresEntities u)
        {
            usersDal.AddUsers(useresEntities.ConvertToDb(u));
        }


    }
}
