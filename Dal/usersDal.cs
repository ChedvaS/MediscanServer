using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class usersDal
    {
        static MedicineProjectEntities db = new MedicineProjectEntities();

        //שליפה
        public static List<USERStbl> Getall()
        {
            return db.USERStbl.ToList();
        }
        //הוספה
        public static void AddCloth(USERStbl u)
        {
            db.USERStbl.Add(u);
            db.SaveChanges();
        }
        //מחיקה
        public static void delete(string gmail)
        {
            db.USERStbl.Remove(db.USERStbl.FirstOrDefault(k => k.GMAIL ==gmail ));
            db.SaveChanges();
        }
        //עידכון
        public static void update(USERStbl u)
        {
             db.USERStbl.FirstOrDefault(x => x.GMAIL == u.GMAIL).FNAME = u.FNAME;
             db.USERStbl.FirstOrDefault(x => x.GMAIL == u.GMAIL).PASS = u.PASS;
             db.SaveChanges();
        }
    }
}
