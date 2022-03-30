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
        public static short AddReminderDetails(REMINDERDETAILStbl r)
        {
            try
            {
                db.REMINDERDETAILStbl.Add(r);
                db.SaveChanges();
                return db.REMINDERDETAILStbl.Last().ID;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        //מחיקה
        public static bool delete(int id)
        {
            var reminderDetailsForDeleting = db.REMINDERDETAILStbl.FirstOrDefault(k => k.ID == id);
            //מחיקה של התזכורות המקושרות לפרטי תזכורת זו
            if (reminderDetailsForDeleting != null)
            {
                foreach (var reminding in db.REMINDERStbl)
                {
                    if (reminding.IDDETAIL == reminderDetailsForDeleting.ID)
                        remindersDal.delete(reminding.ID);
                }
                db.REMINDERDETAILStbl.Remove(reminderDetailsForDeleting);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        //עידכון
        public static void update(REMINDERDETAILStbl r)
        {
            var r_for_delete = db.REMINDERDETAILStbl.FirstOrDefault(x => x.ID == r.ID);
            r_for_delete.IDMEDICINESTOCK = r.IDMEDICINESTOCK;
            r_for_delete.SUBJECTGMAIL = r.SUBJECTGMAIL;
            r_for_delete.COMMENT = r.COMMENT;
            r_for_delete.AMOUNTDAYS = r.AMOUNTDAYS;
            r_for_delete.FREQUINCY = r.FREQUINCY;
            r_for_delete.DOSAGE = r.DOSAGE;
            r_for_delete.STARTDATE = r.STARTDATE;
            r_for_delete.STATUSMEDICINE = r.STATUSMEDICINE;
            db.SaveChanges();
        }

        //פירוט לקיחת תרופה לפי מייל
        public static List<TakingDetails> GetTakingDetailsByGmail(string gmail)
        {
            List<TakingDetails> listTakeD = new List<TakingDetails>();
            var listTd = db.REMINDERStbl.Where(x => x.GMAIL == gmail).Select(x => new { namemed = x.REMINDERDETAILStbl.MEDICINESTOCKtbl.MEDICINEtbl.NAMEMEDICINE, NamePatient = x.USERStbl.FNAME, Freqiency = x.REMINDERDETAILStbl.FREQUINCY, startDate = x.REMINDERDETAILStbl.STARTDATE, comment = x.REMINDERDETAILStbl.COMMENT, numDays = x.REMINDERDETAILStbl.AMOUNTDAYS });

            foreach (var item in listTd)
            {
                double nDays = double.Parse(item.numDays.ToString());
                double LeftD = (item.startDate.Value.AddDays(nDays) - DateTime.Today).TotalDays;

                listTakeD.Add(new TakingDetails { MedicineName = item.namemed, NamePatient = item.NamePatient, frequincy = short.Parse(item.Freqiency.ToString()), LeftDays = short.Parse(LeftD.ToString()), comment = item.comment });
            }
            return listTakeD;
        }


    }
}
