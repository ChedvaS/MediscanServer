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
        public static void AddCloth(REMINDERStbl r)
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
             db.SaveChanges();
        }
    }
}
