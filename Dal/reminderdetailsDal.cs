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
        public static void AddReminderDetails(REMINDERDETAILStbl r)
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

        //פירוט לקיחת תרופה לפי מייל
        public static List<TakingDetails> GetTakingDetailsByGmail(string gmail)
        {
            List<TakingDetails> listTakeD = new List<TakingDetails>();
            var listTd = db.REMINDERStbl.Where(x => x.GMAIL == gmail).Select(x => new { namemed = x.REMINDERDETAILStbl.MEDICINESTOCKtbl.MEDICINEtbl.NAMEMEDICINE, NamePatient = x.USERStbl.FNAME, Freqiency = x.REMINDERDETAILStbl.FREQUINCY , startDate = x.REMINDERDETAILStbl.STARTDATE , comment =x.REMINDERDETAILStbl.COMMENT, numDays = x.REMINDERDETAILStbl.AMOUNTDAYS });

            foreach (var item in listTd)
            {
                double nDays = double.Parse(item.numDays.ToString());
                double LeftD = (item.startDate.Value.AddDays(nDays) - DateTime.Today).TotalDays;

                listTakeD.Add(new TakingDetails { MedicineName=item.namemed, NamePatient =item.NamePatient, frequincy= short.Parse(item.Freqiency.ToString()), LeftDays= short.Parse(LeftD.ToString()),comment=item.comment});
            }
            return listTakeD;
        }


    }
}
