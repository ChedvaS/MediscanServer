using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class reminderdetailsEntities
    {

        public short id { get; set; }
        public Nullable<short> idMedicineStock { get; set; }
        public string subjectGmail { get; set; }
        public string comment { get; set; }
        public Nullable<short> amountDays { get; set; }
        public Nullable<short> frequincy { get; set; }
        public string dosage { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<bool> statusMedicine { get; set; }





        //המרה מאוביקט מסוג מסד נתונים לאובייקט מסוג אנטיטיז
        public static reminderdetailsEntities convertToEntities(REMINDERDETAILStbl r)
        {
            return new reminderdetailsEntities()
            {
                id = r.ID,
                idMedicineStock = r.IDMEDICINESTOCK,
               subjectGmail = r.SUBJECTGMAIL,
               comment = r.COMMENT,
               amountDays=r.AMOUNTDAYS,
               frequincy=r.FREQUINCY,
               dosage=r.DOSAGE,
               startDate=r.STARTDATE,
               statusMedicine=r.STATUSMEDICINE

            };
        }
        //המרה מסוג אנטיטיז לסוג מסד נתונים
        public static REMINDERDETAILStbl ConvertToDb(reminderdetailsEntities r)
        {
            return new REMINDERDETAILStbl()
            {
                ID = r.id,
                IDMEDICINESTOCK = r.idMedicineStock,
                SUBJECTGMAIL  = r.subjectGmail,
                COMMENT = r.comment,
                AMOUNTDAYS = r.amountDays,
                FREQUINCY = r.frequincy,
                DOSAGE = r.dosage,
                STARTDATE = r.startDate,
                STATUSMEDICINE = r.statusMedicine
            };
        }
        //המרה מסוג קשימת מסד נתונים לסוג רשמית אנטיטיז
        public static List<reminderdetailsEntities> ConvertToListEntities(List<REMINDERDETAILStbl> lr)
        {
            List<reminderdetailsEntities> lr1 = new List<reminderdetailsEntities>();
            foreach (var item in lr)
            {
                lr1.Add(convertToEntities(item));
            }
            return lr1;
        }
        //המרה מסוג רשימת אנטיטיז לרשימת מסד נתוניפ
        public static List<REMINDERDETAILStbl> ConvertToListDb(List<reminderdetailsEntities> lr)
        {
            List<REMINDERDETAILStbl> lr1 = new List<REMINDERDETAILStbl>();
            foreach (var item in lr)
            {
                lr1.Add(ConvertToDb(item));
            }
            return lr1;
        }
    }
}
