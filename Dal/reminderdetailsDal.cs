    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class reminderdetailsDal
    {
        static MedicineProjectEntities db = new MedicineProjectEntities();

        //שליפה
        public static List<REMINDERDETAILStbl> Getall()
        {
            return db.REMINDERDETAILStbl.ToList();
        }
        //הוספה
        public static void AddCloth(REMINDERDETAILStbl r)
        {
            db.REMINDERDETAILStbl.Add(r);
            db.SaveChanges();
        }
        //מחיקה
        public static void delete(int id)
        {
            db.REMINDERDETAILStbl.Remove(db.REMINDERDETAILStbl.FirstOrDefault(k => k.ID == id));
            db.SaveChanges();
        }
        //עידכון
        public static void update(REMINDERDETAILStbl r)
        {      
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).IDMEDICINESTOCK = r.IDMEDICINESTOCK;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).SUBJECTGMAIL = r.SUBJECTGMAIL;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).COMMENT = r.COMMENT;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).AMOUNTDAYS = r.AMOUNTDAYS;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).FREQUINCY = r.FREQUINCY;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).DOSAGE = r.DOSAGE;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).STARTDATE = r.STARTDATE;
            db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID).STATUSMEDICINE = r.STATUSMEDICINE;
            db.SaveChanges();
        }

    }
}
