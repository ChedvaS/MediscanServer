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
        public static void addUser(useresEntities u)
        {
            usersDal.AddUsers(useresEntities.ConvertToDb(u));
        }
      
        //שליפה לפי שם משתמש
        public static useresEntities GetUserById(string gmail)
        {
            var user = usersDal.Getall().FirstOrDefault(x => x.GMAIL == gmail);
            if (user==null)
                return null;
            return useresEntities.convertToEntities(user);

        }
    }
}
